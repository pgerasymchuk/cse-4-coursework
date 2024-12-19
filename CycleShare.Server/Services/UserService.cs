using System.Collections;
using AutoMapper;
using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Helpers;
using CycleShare.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace CycleShare.Server.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public UserService(DbContext context, 
                           IOptions<AppSettings> appSettings,
                           IMailService mailService,
                           IMapper mapper)
        {
            this._context = context;
            this._appSettings = appSettings.Value;
            this._mailService = mailService;
            this._mapper = mapper;
        }
        public User GetById(int userId)
        {
            return _context.Set<User>().SingleOrDefault(u => u.Id == userId);
        }

        public User GetByEmail(string email)
        {
            return _context.Set<Credentials>()
                .Include(c => c.User)
                .SingleOrDefault(c => c.User.Email == email)
                .User;
        }

        /*
        public User GetByEmail(string email, string[] includes = null)
        {
            throw new NotImplementedException();
        } 
        
        public User CreateUser(User user, string token, string password)
        {
            throw new NotImplementedException();
        }*/

        public User CreateUser(SignUpDto signUpDto)
        {
            if (_context.Set<Credentials>()
                        .Include(c => c.User)
                        .Any(c => c.User.Email == signUpDto.Email))
            {
                throw new InvalidOperationException("User with such login already exists");
            }

            User user = _mapper.Map<User>(signUpDto);

            byte[] passwordHash, passwordSalt;
            PasswordUtils.CreatePasswordHash(signUpDto.Password, out passwordHash, out passwordSalt);

            var credentials = new Credentials
            {
                Password = passwordHash,
                Salt = passwordSalt,
                User = user,
                UserId = user.Id
            };

            _context.Set<User>().Add(user);
            _context.Set<Credentials>().Add(credentials);
            _context.SaveChanges();

            return user;
        }

        public void ForgotPassword(string email)
        {
            var user = GetByEmail(email);
            if (user != null)
            {
                var token = new ResetPasswordToken
                {
                    Email = email,
                    UserId = user.Id,
                    Token = Guid.NewGuid().ToString().Replace("-", ""),
                    ExpirationDate = DateTime.UtcNow.AddHours(_appSettings.ForgotPasswordSettings.ExpiryTimeInHours)
                };
                _context.Set<ResetPasswordToken>().Add(token);
                _context.SaveChanges();

                _mailService.SendForgotPasswordEmail(new EmailDto
                {
                    EmailAddress = user.Email,
                    Token = token.Token,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
        }

        public User ResetPassword(string token, string password)
        {
            var validToken = _context.Set<ResetPasswordToken>()
                                     .SingleOrDefault(t => t.Token == token && !t.Verified && (t.ExpirationDate > DateTime.UtcNow));
            if (validToken == null)
            {
                throw new ArgumentException("ResetPasswordToken is invalid");
            }

            var c = _context.Set<Credentials>()
                .Include(c => c.User)
                .SingleOrDefault(c => c.UserId == validToken.UserId);

            if (c == null)
            {
                throw new KeyNotFoundException("User not found while reseting password");
            }

            byte[] passwordHash, passwordSalt;
            PasswordUtils.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            c.Password = passwordHash;
            c.Salt = passwordSalt;

            validToken.Verified = true;
            _context.SaveChanges();
            return c.User;
        }
    }
}
