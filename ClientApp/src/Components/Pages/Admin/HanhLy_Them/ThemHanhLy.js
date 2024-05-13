﻿import React, { useState } from "react";
import './ThemHanhLy.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';

const ThemHanhLy = () => {
    const [luggageCode, setLuggageCode] = useState("");
    const [mass, setMass] = useState("");
    const [price, setPrice] = useState("");


    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid luggage data");
            return;
        }

        const luggageData = {
            luggageCode: luggageCode,
            mass: mass,
            price: price,
        };
        try {
        const luggageResponse = await fetch("api/luggage/AddLuggage", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(luggageData),
        });
            if (!luggageResponse.ok) {
                const luggageError = await luggageResponse.json();
                console.error("Luggage error:", luggageError);
                alert("Failed to add Luggage");
                return;
            }

            alert("Luggage added successfully");
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const isValidData = () => {
        return (
            luggageCode.trim() !== ""
        );
    };

    return (
        <div className="container-fluid">
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Thêm khách hàng</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="luggageCode" className="form-label">Mã hành lý</label>
                            <input
                                type="text"
                                className="form-control"
                                id="luggageCode"
                                placeholder="Mã hành lý"
                                value={luggageCode}
                                onChange={(e) => setLuggageCode(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="mass" className="form-label">Cân nặng</label>
                            <input
                                type="text"
                                className="form-control"
                                id="mass"
                                placeholder="Cân nặng"
                                value={mass}
                                onChange={(e) => setMass(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="price" className="form-label">Giá tiền</label>
                            <input
                                type="text"
                                className="form-control"
                                id="price"
                                placeholder="price"
                                value={price}
                                onChange={(e) => setPrice(e.target.value)}
                            />
                        </div>
                    </div>
                    
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave}>Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./HanhLy" className="text-decoration-underline-mk">Quay lại trang dành cho hành lý</a>
            </div>
        </div>
    );
}

export default ThemHanhLy;
