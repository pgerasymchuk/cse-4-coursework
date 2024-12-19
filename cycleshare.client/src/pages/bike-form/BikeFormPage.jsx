import React, { useState, useEffect } from "react";
import {
    TextField,
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    Button,
    Typography,
    Grid,
    Box,
} from "@mui/material";
import { styled } from "@mui/system";
import { useParams } from "react-router-dom";

const ImagePreview = styled("img")({
    width: "100px",
    height: "100px",
    objectFit: "cover",
    marginRight: "10px",
});

const BikeFormPage = ({ onSubmit }) => {
    const { bikeId } = useParams();
    const [formData, setFormData] = useState({
        name: "",
        description: "",
        price: "",
        type: "",
        material: "",
        wheelsize: "",
        images: [],
        city: "",
        street: "",
        building: "",
    });
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (bikeId) {
            setLoading(true);
            fetch(`http://localhost:5078/api/bike/${bikeId}`)
                .then((response) => {
                    if (!response.ok) {
                        throw new Error("Failed to fetch bike details");
                    }
                    return response.json();
                })
                .then((data) => {
                    setFormData({
                        id: data.id || "",
                        name: data.name || "",
                        description: data.description || "",
                        price: data.price || "",
                        type: data.type || "",
                        material: data.material || "",
                        wheelsize: data.wheelsize || "",
                        images: data.images || [],
                        city: data.city || "",
                        street: data.street || "",
                        building: data.building || "",
                        note: data.note || "",
                    });
                    setLoading(false);
                })
                .catch((error) => {
                    console.error(error);
                    setLoading(false);
                });
        }
    }, [bikeId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleFileChange = (e) => {
        const files = Array.from(e.target.files);
        setFormData({
            ...formData,
            images: [...formData.images, ...files],
        });
    };

    const handleRemoveImage = (index) => {
        const updatedImages = formData.images.filter((_, i) => i !== index);
        setFormData({ ...formData, images: updatedImages });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const endpoint = bikeId
            ? `http://localhost:5078/api/bike/${bikeId}`
            : "http://localhost:5078/api/bike";
        const method = bikeId ? "PUT" : "POST";

        const body = new FormData();
        Object.entries(formData).forEach(([key, value]) => {
            if (key === "images") {
                value.forEach((file) => body.append("images", file)); 
            } else {
                body.append(key, value);
            }
        });

        fetch(endpoint, {
            method,
            body,
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to save bike details");
                }
                return response.json();
            })
            .then((data) => {
                console.log("Bike saved successfully:", data);
                onSubmit(data);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    if (loading) {
        return <Typography>Loading...</Typography>;
    }

    return (
        <Box p={3}>
            <Typography variant="h4" gutterBottom>
                {bikeId ? "Edit Bicycle" : "Create Bicycle"}
            </Typography>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={3}>
                    {/* Existing Fields */}
                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="Name"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="Price"
                            name="price"
                            value={formData.price}
                            onChange={handleChange}
                            type="number"
                            required
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            label="Description"
                            name="description"
                            value={formData.description}
                            onChange={handleChange}
                            multiline
                            rows={4}
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <FormControl fullWidth>
                            <InputLabel>Type</InputLabel>
                            <Select
                                name="type"
                                value={formData.type}
                                onChange={handleChange}
                                required
                            >
                                <MenuItem value="Road">Road</MenuItem>
                                <MenuItem value="Hybrid">Hybrid</MenuItem>
                                <MenuItem value="Mountain">Mountain</MenuItem>
                                <MenuItem value="">Other</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <FormControl fullWidth>
                            <InputLabel>Material</InputLabel>
                            <Select
                                name="material"
                                value={formData.material}
                                onChange={handleChange}
                                required
                            >
                                <MenuItem value="Aluminium">Aluminium</MenuItem>
                                <MenuItem value="Steel">Steel</MenuItem>
                                <MenuItem value="Carbon">Carbon</MenuItem>
                                <MenuItem value="">Other</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <FormControl fullWidth>
                            <InputLabel>Wheel Size</InputLabel>
                            <Select
                                name="wheelsize"
                                value={formData.wheelsize}
                                onChange={handleChange}
                                required
                            >
                                <MenuItem value="24 in">24 in</MenuItem>
                                <MenuItem value="26 in">26 in</MenuItem>
                                <MenuItem value="28 in">28 in</MenuItem>
                                <MenuItem value="29 in">29 in</MenuItem>
                                <MenuItem value="">Other</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>

                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="City"
                            name="city"
                            value={formData.city}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="Street"
                            name="street"
                            value={formData.street}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="Building"
                            name="building"
                            value={formData.building}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <TextField
                            fullWidth
                            label="Note"
                            name="note"
                            value={formData.note}
                            onChange={handleChange}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <Button
                            variant="contained"
                            component="label"
                            color="primary"
                        >
                            Upload Images
                            <input
                                type="file"
                                hidden
                                multiple
                                accept="image/*"
                                onChange={handleFileChange}
                            />
                        </Button>
                    </Grid>
                    <Grid item xs={12}>
                        <Box display="flex" flexWrap="wrap" gap={2} mt={2}>
                            {formData.images.map((image, index) => (
                                <Box key={index} position="relative">
                                    {typeof image === "string" ? (
                                        <ImagePreview src={image} alt={`uploaded-${index}`} />
                                    ) : (
                                        <ImagePreview
                                            src={URL.createObjectURL(image)}
                                            alt={`uploaded-${index}`}
                                        />
                                    )}
                                    <Button
                                        size="small"
                                        color="secondary"
                                        onClick={() => handleRemoveImage(index)}
                                    >
                                        Remove
                                    </Button>
                                </Box>
                            ))}
                        </Box>
                    </Grid>

                    <Grid item xs={12}>
                        <Button type="submit" variant="contained" color="primary">
                            {bikeId ? "Update Bicycle" : "Create Bicycle"}
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </Box>
    );
};

export default BikeFormPage;