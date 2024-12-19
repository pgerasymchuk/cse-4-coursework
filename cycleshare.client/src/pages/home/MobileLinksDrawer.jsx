import { Drawer, IconButton } from "@mui/material";
import React from "react";
import CloseIcon from '@mui/icons-material/Close';

const MENU = [
    {path: '/', label: 'Catalog'},
    {path: '/bikes', label: 'My Bikes'},
];

const MobileLinksDrawer = ({ open, onHandleOpen, onNavigate }) => {
    return (
        <Drawer
            className="mobile-drawer hide-in-desktop"
            anchor="left"
            transitionDuration={400}
            open={open}
            onClose={() => {
                onHandleOpen(false);
            }}
            sx={{
                display: "flex",
                flexDirection: "column",
                padding: "30px",
                height: "100vh",
            }}
        >
            <div className="draw" style={{ width: "65vw" }}>
                <section className="closing">
                    <IconButton
                        disableRipple
                        onClick={() => {
                            onHandleOpen(false);
                        }}
                    >
                        <CloseIcon fillColor={"#68707d"} />
                    </IconButton>
                </section>
                <section className="mobile-links">
                    <ul>
                        {MENU.map(item => (
                            <li>
                                <button onClick={() => onNavigate(item.path)}>{item.label}</button>
                            </li>
                        ))}
                    </ul>
                </section>
            </div>
        </Drawer>
    );
};

export default MobileLinksDrawer;
