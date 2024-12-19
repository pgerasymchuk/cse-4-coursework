import React, {useState} from "react";
import MobileLinksDrawer from "./MobileLinksDrawer";
import DirectionsBikeIcon from '@mui/icons-material/DirectionsBike';
import IconButton from "@mui/material/IconButton";
import MenuIcon from '@mui/icons-material/Menu';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import MenuItem from "@mui/material/MenuItem";
import Menu from "@mui/material/Menu";
import {Divider, ListItemIcon, ListItemText, MenuList, Paper} from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';

const MENU = [
    {path: '/', label: 'Catalog'},
    {path: '/bikes', label: 'My Bikes'},
];

const Header = ({onLogout, onNavigate}) => {
    const [open, setOpen] = useState(false);
    const [anchorEl, setAnchorEl] = useState(null);
    const openAccountMenu = Boolean(anchorEl);
    const handleMobileMenuOpen = (val) => {
        setOpen(val);
    };
    const handleAccountMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleAccountMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <header>
            <nav>
                <section className="left">
                    <div className="imgs">
                        <IconButton
                            alt="icon-menu"
                            className="hide-in-desktop"
                            onClick={() => {
                                handleMobileMenuOpen(true);
                            }}>
                            <MenuIcon/>
                        </IconButton>
                        <MobileLinksDrawer onHandleOpen={handleMobileMenuOpen} onOpen={open} onNavigate={onNavigate}/>
                        <DirectionsBikeIcon fontSize={"large"}/>
                    </div>
                    <div className="links hide-in-mobile">
                        <ul>
                            {MENU.map(item => (
                                <li>
                                    <button onClick={() => onNavigate(item.path)}>{item.label}</button>
                                </li>
                            ))}
                        </ul>
                    </div>
                </section>
                <div className="right">
                    <IconButton onClick={handleAccountMenuOpen}
                                aria-controls={open ? 'basic-menu' : undefined}
                                aria-haspopup="true"
                                aria-expanded={open ? 'true' : undefined}>
                        <AccountCircleIcon fontSize={'large'} alt="img-avatar" className="avatar"/>
                    </IconButton>
                    <Menu
                        id="basic-menu"
                        anchorEl={anchorEl}
                        open={openAccountMenu}
                        onClose={handleAccountMenuClose}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'right',
                        }}
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'right',
                        }}
                        MenuListProps={{
                            'aria-labelledby': 'basic-button',
                        }}
                    >
                        <MenuList sx={{width: 160, maxWidth: '100%'}}>
                            <MenuItem onClick={onNavigate.bind(null, '/profile')}>
                                <ListItemIcon>
                                    <AccountCircleIcon fontSize="small"/>
                                </ListItemIcon>
                                <ListItemText>Profile</ListItemText>
                            </MenuItem>
                            <Divider/>
                            <MenuItem onClick={onLogout}>
                                <ListItemIcon>
                                    <LogoutIcon fontSize="small"/>
                                </ListItemIcon>
                                <ListItemText>Logout</ListItemText>
                            </MenuItem>
                        </MenuList>
                    </Menu>
                </div>
            </nav>
        </header>
    );
};

export default Header;
