import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";

const BikeEditorPage = () => {
    const { bikeId } = useParams(); 
    const navigate = useNavigate();

    const [bikeData, setBikeData] = useState({
        id: 0,
        name: "",
        description: "",
        price: 0,
        type: "",
        wheelsize: "",
        material: "",
        userId: 0,
        owner: "", 
        galleryItemsIds: [],
    });

    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (bikeId) {
            setIsLoading(true);
            fetch(`/api/bikes/${bikeId}`)
                .then((response) => response.json())
                .then((data) => {
                    setBikeData({
                        ...data,
                        owner: "John Doe", 
                    });
                    setIsLoading(false);
                })
                .catch((error) => {
                    console.error("Error fetching bike data:", error);
                    setIsLoading(false);
                });
        }
    }, [bikeId]);

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setBikeData((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        const method = bikeId ? "PUT" : "POST";
        const url = bikeId ? `/api/bikes/${bikeId}` : "/api/bikes";

        fetch(url, {
            method,
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(bikeData),
        })
            .then((response) => {
                if (response.ok) {
                    navigate("/bikes"); 
                } else {
                    console.error("Error saving bike data:", response.statusText);
                }
            })
            .catch((error) => {
                console.error("Error saving bike data:", error);
            });
    };

    if (isLoading) return <div>Loading...</div>;

    return (
        <div className="bike-details">
            <h1>{bikeId ? `Edit Bike - ${bikeData.name}` : "Add New Bike"}</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input
                        type="text"
                        id="name"
                        name="name"
                        value={bikeData.name}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        name="description"
                        value={bikeData.description}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="type">Type:</label>
                    <input
                        type="text"
                        id="type"
                        name="type"
                        value={bikeData.type}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="material">Material:</label>
                    <input
                        type="text"
                        id="material"
                        name="material"
                        value={bikeData.material}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="wheelsize">Wheel Size:</label>
                    <input
                        type="text"
                        id="wheelsize"
                        name="wheelsize"
                        value={bikeData.wheelsize}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="owner">Owner:</label>
                    <input
                        type="text"
                        id="owner"
                        name="owner"
                        value={bikeData.owner}
                        onChange={handleInputChange}
                        readOnly
                    />
                </div>
                <div>
                    <button type="submit">{bikeId ? "Update Bike" : "Create Bike"}</button>
                </div>
            </form>
        </div>
    );
};

export default BikeEditorPage;
