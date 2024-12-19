import React from "react";

const Description = ({ item, onBook }) => {
    return (
        <section className="description">
            <p className="pre">sneaker company</p>
            <h1>{item.name}</h1>
            <p className="desc">{item.description}</p>
            <div className="price">
                <div className="main-tag"><p>${item.price}</p></div>
            </div>
            <div className="buttons">
                {/*<QuantityButton onQuant={onQuant} onRemove={onRemove} onAdd={onAdd} />*/}
                <button
                    className="add-to-cart"
                    onClick={() => {
                        onBook(item);
                    }}
                >
                    {/*<CartIcon />*/}
                    Book
                </button>
            </div>
        </section>
    );
};

export default Description;
