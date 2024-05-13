﻿import React, { useState } from "react";
import './ThemMaGiamGia.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';


const formatDate = (date) => {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${day}-${month}-${year}`;
};

const ThemMaGiamGia = () => {
    const [dId, setDId] = useState("");
    const [dName, setDName] = useState("");
    const [dStart, setDStart] = useState("");
    const [dFinish, setDFinish] = useState("");
    const [dPercent, setDPercent] = useState("");

    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid customer data");
            return;
        }

        const discountData = {
            dId: dId,
            dName: dName,
            dStart: formatDate(new Date(dStart)),
            dFinish: formatDate(new Date(dFinish)),
            dPercent: dPercent,
        };
        console.log('Start Date:', discountData.startDate);
        console.log('End Date:', discountData.endDate);

        try {
        const discountResponse = await fetch("api/discount/AddDiscount", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(discountData),
        });
            if (!discountResponse.ok) {
                const discountError = await discountResponse.json();
                console.error("Discount error:", discountError);
                alert("Failed to add Discount");
                return;
            }

            alert("Discount added successfully");
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const isValidData = () => {
        return (
            dId.trim() !== ""
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
                <h2>Thêm khuyến mãi</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="dId" className="form-label">Mã khuyến mãi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="dId"
                                placeholder="Mã khuyến mãi"
                                value={dId}
                                onChange={(e) => setDId(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="dName" className="form-label">Tên khuyến mãi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="dName"
                                placeholder="Tên khuyến mãi"
                                value={dName}
                                onChange={(e) => setDName(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="dStart" className="form-label">Ngày bắt đầu</label>
                            <input
                                type="date"
                                className="form-control"
                                id="dStart"
                                placeholder="Ngày bắt đầu"
                                value={dStart}
                                onChange={(e) => setDStart(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="dFinish" className="form-label">Ngày kết thúc</label>
                            <input
                                type="date"
                                className="form-control"
                                id="dFinish"
                                placeholder="Ngày kết thúc"
                                value={dFinish}
                                onChange={(e) => setDFinish(e.target.value)}
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="dPercent" className="form-label">Phần trăm</label>
                            <input
                                type="number"
                                className="form-control"
                                id="dPercent"
                                placeholder="Phần trăm"
                                value={dPercent}
                                onChange={(e) => setDPercent(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave}>Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./MaGiamGia" className="text-decoration-underline-mk">Quay lại trang dành cho khuyến mãi</a>
            </div>
        </div>
    );
}

export default ThemMaGiamGia;
