import React, {useState, useEffect} from "react";
import {
    Container,
    Grid,
    Card,
    CardContent,
    CardMedia,
    Typography,
    Rating
} from "@mui/material";
import {styled} from "@mui/system";
import {useNavigate} from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import {useSearchParams} from 'react-router-dom';
import CatalogFilters from "./CatalogFilters";

const StyledCard = styled(Card)(({theme}) => ({
    height: "100%",
    display: "flex",
    flexDirection: "column",
    transition: "transform 0.2s",
    cursor: "pointer",
    "&:hover": {
        transform: "scale(1.02)"
    }
}));

const DEFAULT_FILTERS = {
    materials: ["Steel"]
};
const ProductList = () => {
    // const initialProducts = [
    //     {
    //         id: 1,
    //         name: "Premium Laptop",
    //         price: 999.99,
    //         rating: 4.5,
    //         category: "Electronics",
    //         image: "https://images.unsplash.com/photo-1496181133206-80ce9b88a853"
    //     },
    //     {
    //         id: 2,
    //         name: "Wireless Headphones",
    //         price: 199.99,
    //         rating: 4.0,
    //         category: "Electronics",
    //         image: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e"
    //     },
    //     {
    //         id: 3,
    //         name: "Running Shoes",
    //         price: 89.99,
    //         rating: 4.8,
    //         category: "Sports",
    //         image: "https://images.unsplash.com/photo-1542291026-7eec264c27ff"
    //     },
    //     {
    //         id: 4,
    //         name: "Coffee Maker",
    //         price: 79.99,
    //         rating: 4.2,
    //         category: "Appliances",
    //         image: "https://images.unsplash.com/photo-1517701604599-bb29b565090c"
    //     }
    // ];
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [items, setItems] = useState([]);
    const navigate = useNavigate();

    // useEffect(() => {
    //     const params = new URLSearchParams(searchParams);
    //     let updated = false;
    //
    //     for (const [key, value] of Object.entries(DEFAULT_FILTERS)) {
    //         if (!params.has(key)) {
    //             params.set(key, value);
    //             updated = true;
    //         }
    //     }
    //
    //     console.log('searchParams', params);
    //     if (updated) {
    //         setSearchParams(params);
    //     }
    // }, []); // Run once on mount

    const handleParam = (params, key, value) => {
        if (value) {
            if (Array.isArray(value)) {
                params.delete(key);
                for (let v of value) {
                    params.append(key, v);
                }
            } else {
                params.set(key, value);
            }
        } else {
            params.delete(key);
        }
    }

    const handleFilterChange = (key, value) => {
        console.log(key, value);
        const params = new URLSearchParams(searchParams);
        if (Array.isArray(key)) {
            for (let pair of key) {
                handleParam(params, pair[0], pair[1]);
            }
        } else {
            handleParam(params, key, value);
        }
        setSearchParams(params);
    };

    const getFiltersFromQuery = (params) => {
        const filters = {...DEFAULT_FILTERS}; // Start with default filters
        for (const [key, value] of params.entries()) {
            filters[key] = value;
        }
        return filters;
    };

    useEffect(() => {
        // const selectedCategories = Object.entries(categories)
        //     .filter(([_, selected]) => selected)
        //     .map(([category]) => category);
        //
        // if (selectedCategories.length > 0) {
        //     params = {
        //         ...params,
        //     }
        //     filtered = filtered.filter(product =>
        //         selectedCategories.includes(product.category)
        //     );
        // }
        const fetchAds = async () => {
            try {
                setLoading(true);
                const params = getFiltersFromQuery(searchParams);
                const response = await axiosInstance.get(`/api/bike`, {params});
                setItems(response.data);
                // setItems(initialProducts.map(item => ({
                //     ...item,
                //     rating: 4.5,
                //     category: "Electronics",
                //     image: "https://images.unsplash.com/photo-1496181133206-80ce9b88a853"
                // })));
            } catch (err) {
                setError('Failed to fetch ad details.');
            } finally {
                setLoading(false);
            }
        };
        fetchAds();
    }, [searchParams]);

    const handleItemClick = (item) => {
        navigate(`/ads/${item.id}`);
    }

    return (
        <Container maxWidth="lg" sx={{py: 4}}>
            <Grid container spacing={4}>
                <Grid item xs={12} md={3}>
                    <CatalogFilters searchParams={searchParams} onFilterChange={handleFilterChange}/>
                </Grid>

                {loading ? <div>Loading...</div> : null}
                {error ? <div>Error: {error}</div> : null}

                {!loading && !error ?
                    (<Grid item xs={12} md={9}>
                        <Grid container spacing={3}>
                            {items.map(item => (
                                <Grid item key={item.id} xs={12} sm={6} md={4}>
                                    <StyledCard onClick={() => handleItemClick(item)}>
                                        <CardMedia
                                            component="img"
                                            height="200"
                                            image={item.image}
                                            alt={item.name}
                                            onError={(e) => {
                                                e.target.src = "https://images.unsplash.com/photo-1524758631624-e2822e304c36";
                                            }}
                                        />
                                        <CardContent>
                                            <Typography gutterBottom variant="h6" component="div">
                                                {item.name}
                                            </Typography>
                                            <Typography variant="body2" color="text.secondary">
                                                ${item.price.toFixed(2)}
                                            </Typography>
                                            <Rating
                                                value={item.rating}
                                                precision={0.5}
                                                readOnly
                                                size="small"
                                            />
                                            <Typography variant="body2" color="text.secondary">
                                                {item.category}
                                            </Typography>
                                        </CardContent>
                                    </StyledCard>
                                </Grid>
                            ))}
                        </Grid>
                    </Grid>) : null}
            </Grid>
        </Container>
    );
};

const Catalog = () => {
    return <ProductList/>
};

export default Catalog;
