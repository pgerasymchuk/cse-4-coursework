import * as React from 'react';
import {Dialog, DialogTitle, DialogContent, DialogContentText, OutlinedInput, DialogActions, Button} from "@mui/material";
import axiosInstance from "../../utils/axiosInstance";

export default function ForgotPassword({ open, handleClose }) {
  return (
    <Dialog
      open={open}
      onClose={handleClose}
      PaperProps={{
        component: 'form',
        onSubmit: async (event) => {
          event.preventDefault();
          event.stopPropagation();
          const email = new FormData(event.currentTarget).get('email');
          console.log(email)
          try {
            await axiosInstance.post('/api/user/password/forgot/', email)
          } catch(e) {
            console.log(e);
          } finally {
            handleClose();
          }
        },
        sx: { backgroundImage: 'none' },
      }}
    >
      <DialogTitle>Reset password</DialogTitle>
      <DialogContent
        sx={{ display: 'flex', flexDirection: 'column', gap: 2, width: '100%' }}
      >
        <DialogContentText>
          Enter your account&apos;s email address, and we&apos;ll send you a link to
          reset your password.
        </DialogContentText>
        <OutlinedInput
          autoFocus
          required
          margin="dense"
          id="email"
          name="email"
          label="Email address"
          placeholder="Email address"
          type="email"
          fullWidth
        />
      </DialogContent>
      <DialogActions sx={{ pb: 3, px: 3 }}>
        <Button onClick={handleClose}>Cancel</Button>
        <Button variant="contained" type="submit">
          Continue
        </Button>
      </DialogActions>
    </Dialog>
  );
}
