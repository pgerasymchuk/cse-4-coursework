import React, {useEffect, useState} from "react";
import Gallery from "./Gallery";
import MobileGallery from "./MobileGallery";
import Description from "./Description";
import {useParams} from "react-router-dom";

const Advertisement = () => {
    const [advertisement, setAdvertisement] = useState(null);
    const { id } = useParams();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAd = async () => {
            try {
                setLoading(true);
                // const response = await axiosInstance().get(`/api/products/${id}`);
                const timeoutId = setTimeout(() => {
                    setAdvertisement({
                        "name": "Specialized Rockhopper",
                        "description": "A robust mountain bike equipped to handle rough terrains, featuring a sturdy aluminum frame and responsive suspension.",
                        "type": "mountain",
                        "material": "aluminum",
                        "wheelSize": 29,
                        "price": 450,
                        "images": [
                            "https://images.unsplash.com/photo-1505740420928-5e560c06d30e",
                            "https://images.unsplash.com/photo-1542291026-7eec264c27ff",
                            "https://images.unsplash.com/photo-1517701604599-bb29b565090c"
                        ]
                    });

                    setLoading(false);
                }, 2000);
                // setAdvertisement(response.data);
                return () => clearTimeout(timeoutId);
            } catch (err) {
                setError('Failed to fetch ad details.');
            } finally {
                // setLoading(false);
            }
        };

        fetchAd();
    }, [id]);

    const handleBooking = () => console.log('Book bike');

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <section className="core">
            <Gallery images={advertisement.images}/>
            <MobileGallery images={advertisement.images}/>
            <Description item={advertisement} onBook={handleBooking}/>
        </section>
    );
};

export default Advertisement;
