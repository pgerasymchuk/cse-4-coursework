import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import {
    Container,
    Typography,
    Card,
    CardMedia,
    CardContent,
    Grid,
    CircularProgress,
    Box,
} from "@mui/material";

const BikeDetailsPage = () => {
    const { bikeId } = useParams(); 
    const [bike, setBike] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchBikeDetails = async () => {
            try {
                const response = await axios.get(`http://localhost:5078/api/bike/${bikeId}`);
                setBike(response.data);
            } catch (err) {
                console.error("Error fetching bike details:", err);
                setError("Failed to fetch bike details. Please try again later.");
            } finally {
                setLoading(false);
            }
        };
        fetchBikeDetails();
    }, [bikeId]);

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="50vh">
                <CircularProgress />
            </Box>
        );
    }

    if (error) {
        return (
            <Container>
                <Typography variant="h6" color="error" align="center">
                    {error}
                </Typography>
            </Container>
        );
    }

    return (
        <Container>
            <Grid container spacing={4} mt={4}>
                <Grid item xs={12} md={6}>
                    <Card>
                        <CardMedia
                            component="img"
                            image={bike.imageUrl || "/images/1.jpg"} 
                            alt={bike.name}
                            sx={{ maxHeight: 400 }}
                        />
                    </Card>
                </Grid>

                <Grid item xs={12} md={6}>
                    <Typography variant="h4" gutterBottom>
                        {bike.name}
                    </Typography>
                    <Typography variant="body1" gutterBottom>
                        {bike.description}
                    </Typography>
                    <Typography variant="h6" gutterBottom>
                        Type: {bike.type}
                    </Typography>
                    <Typography variant="h6" gutterBottom>
                        Material: {bike.material}
                    </Typography>
                    <Typography variant="h6" gutterBottom>
                        Wheel Size: {bike.wheelsize}
                    </Typography>
                    <Typography variant="h6" gutterBottom>
                        Owner: User #{bike.userId}
                    </Typography>
                    <Typography variant="h6" color="primary" gutterBottom>
                        Price: ${bike.price.toFixed(2)} / day
                    </Typography>
                </Grid>
            </Grid>
        </Container>
    );
};

export default BikeDetailsPage;
