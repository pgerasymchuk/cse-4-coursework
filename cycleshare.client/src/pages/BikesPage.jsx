import React, { useState, useEffect } from "react";
import axios from 'axios';
import { Link } from "react-router-dom";
import {
    Card,
    CardMedia,
    CardContent,
    Typography,
    Grid,
    Box,
    Container,
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    Pagination,
    Button,
} from "@mui/material";
import axiosInstance from "../utils/axiosInstance";

const BikesPage = () => {
    const [bikes, setBikes] = useState([]);
    const [sortBy, setSortBy] = useState("CreatedDate");
    const [order, setOrder] = useState("asc");
    const [type, setType] = useState("");
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(9);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        const fetchBikes = async () => {
            const params = {
                sortBy,
                order,
                type,
                pageNumber,
                pageSize,
            };
            try {
                const response = await axiosInstance.get('/api/bike', { params });
                setBikes(response.data);
                setTotalPages(Math.ceil(response.data.totalCount / pageSize));
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchBikes();
    }, [sortBy, order, type, pageNumber, pageSize]);

    const handleSortChange = (event) => {
        const [sortField, sortOrder] = event.target.value.split(",");
        setSortBy(sortField);
        setOrder(sortOrder);
    };

    const handleFilterChange = (event) => {
        setType(event.target.value);
    };

    const handlePageChange = (event, newPage) => {
        setPageNumber(newPage);
    };

    return (
        <Container>
            <Typography variant="h4" align="center" gutterBottom>
                Bike Adverts
            </Typography>

            <Grid container spacing={2} mb={4}>
                <Grid item xs={12} sm={4}>
                    <FormControl fullWidth>
                        <InputLabel>Sort By</InputLabel>
                        <Select
                            value={`${sortBy},${order}`}
                            onChange={handleSortChange}
                            label="Sort By"
                        >
                            <MenuItem value="CreatedDate,asc">Date (Oldest to Newest)</MenuItem>
                            <MenuItem value="CreatedDate,desc">Date (Newest to Oldest)</MenuItem>
                            <MenuItem value="Price,asc">Price (Low to High)</MenuItem>
                            <MenuItem value="Price,desc">Price (High to Low)</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12} sm={4}>
                    <FormControl fullWidth>
                        <InputLabel>Type</InputLabel>
                        <Select
                            value={type}
                            onChange={handleFilterChange}
                            label="Type"
                        >
                            <MenuItem value="">All Types</MenuItem>
                            <MenuItem value="Road">Road</MenuItem>
                            <MenuItem value="Hybrid">Hybrid</MenuItem>
                            <MenuItem value="Mountain">Mountain</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
            </Grid>
            { console.log(bikes)}
            <Grid container spacing={3}>
                {bikes.map((bike) => (
                    <Grid item xs={12} sm={6} md={4} key={bike.id}>
                        <Link to={`/bikes/${bike.id}`} style={{ textDecoration: "none" }}>
                            <Card>
                                <CardMedia
                                    component="img"
                                    image="/images/1.jpg"
                                    alt={bike.name}
                                    sx={{ height: 200 }}
                                />
                                <CardContent>
                                    <Typography variant="h6" gutterBottom>
                                        {bike.name}
                                    </Typography>
                                    <Typography variant="body2" color="text.secondary">
                                        {bike.description}
                                    </Typography>
                                    <Box mt={2}>
                                        <Typography variant="h6" color="primary">
                                            ${bike.price}
                                        </Typography>
                                    </Box>
                                </CardContent>
                            </Card>
                        </Link>
                    </Grid>
                ))}
            </Grid>

            <Box mt={4} display="flex" justifyContent="center">
                <Pagination
                    count={totalPages}
                    page={pageNumber}
                    onChange={handlePageChange}
                    color="primary"
                />
            </Box>
        </Container>
    );
};

export default BikesPage;
