import React, { useState , useEffect } from "react";
import './SuaHanhLy.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';

const SuaHanhLy = () => {
    const location = useLocation();
    const [selectedLuggageInfo, setSelectedLuggageInfo] = useState(location.state?.selectedLuggageInfo || []);

    useEffect(() => {
        console.log("Selected Luggage info in SuaKhachHang useEffect:", selectedLuggageInfo);
        // Các thao tác khác với selectedLuggageInfo
    }, [selectedLuggageInfo]);

    const [luggageInfo, setLuggageInfo] = useState({
        luggageCode: '',
        mass: '',
        price: '',
    });

    useEffect(() => {
        if (selectedLuggageInfo.length > 0) {
            // Nếu có thông tin khách hàng được chọn, cập nhật LuggageInfo
            setLuggageInfo({
                luggageCode: selectedLuggageInfo[0]?.luggageCode || '',
                mass: selectedLuggageInfo[0]?.mass || '',
                price: selectedLuggageInfo[0]?.price || '',
            });
        }
    }, [selectedLuggageInfo]);


    const handleChange = (e) => {
        const { id, value } = e.target;

        setLuggageInfo({
            ...luggageInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {

            if (!luggageInfo || !luggageInfo.luggageCode) {
                alert("Khách hàng không được tìm thấy");
                return;
            }

            const updatedData = {
                luggageCode: luggageInfo.luggageCode,
                mass: luggageInfo.mass,
                price: luggageInfo.price,
            };


            if (!updatedData.luggageCode) {
                alert("LuggageCode là bắt buộc");
                return;
            }

            // Sử dụng fetch để thực hiện yêu cầu PUT
            const response = await fetch('api/luggage/UpdateLuggage', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedData),
            });

            if (!response.ok) {
                // Xử lý lỗi
                const errorMessage = await response.text();
                throw new Error(JSON.stringify(errorMessage));
            }

            alert("Hành lý đã được cập nhật");

        } catch (err) {
            // Xử lý lỗi
            alert(err.message);
        }
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
                <h2>Sửa thông tin hành lý</h2>
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
                                value={luggageInfo.luggageCode}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="mass" className="form-label">Cân nặng</label>
                            <input
                                type="text"
                                className="form-control"
                                id="mass"
                                placeholder="Cân nặng"
                                value={luggageInfo.mass}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="price" className="form-label">Giá tiền</label>
                            <input
                                type="text"
                                className="form-control"
                                id="price"
                                placeholder="price"
                                value={luggageInfo.price}
                                onChange={handleChange}
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

export default SuaHanhLy;
