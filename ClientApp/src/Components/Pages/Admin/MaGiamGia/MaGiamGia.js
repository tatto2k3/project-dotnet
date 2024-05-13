import React, { useEffect, useState } from "react";
import './MaGiamGia.css';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import moment from "moment";
import { toast } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';

const MaGiamGia = () => {
    const [searchKeyword, setSearchKeyword] = useState('');
    const [discounts, setDiscounts] = useState([]);
    const [selectedDiscounts, setSelectedDiscounts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(8);
    const navigate = useNavigate();

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = discounts.slice(indexOfFirstItem, indexOfLastItem);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleClick = () => {
        navigate('/MaGiamGia_Them');
    };



    useEffect(() => {
        // Lấy danh sách khách hàng từ API hoặc nguồn dữ liệu khác
        const fetchData = async () => {
            try {
                const response = await fetch("/api/discount/GetDiscounts");
                const data = await response.json();
                setDiscounts(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);


    const handleCheckboxChange = (dId) => {
        if (selectedDiscounts.includes(dId)) {
            // Nếu đã chọn thì hủy chọn
            setSelectedDiscounts([]);
        } else {
            // Nếu chưa chọn thì chọn
            setSelectedDiscounts([dId]);
        }
    };



    const handleShowInfo = async () => {
        try {
            if (selectedDiscounts.length > 0) {
                const response = await fetch(`/api/discount/GetDiscountDetails?dIds=${selectedDiscounts.join(',')}`);
                const data = await response.json();

                // Chuyển hướng sang trang sửa khách hàng và truyền thông tin khách hàng
                navigate('/MaGiamGia_Sua', { state: { selectedDiscountInfo: data } });
            } else {
                console.log("No discounts selected.");
            }
        } catch (error) {
            console.error("Error fetching discount details:", error);
        }
    };

    const handleDelete = async () => {
        if (selectedDiscounts.length > 0) {
            if (window.confirm("Are you sure to delete this discount")) {
                try {
                    const response = await axios.delete('http://localhost:44430/api/discount', {
                        data: selectedDiscounts, // Pass the array as data
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    });

                    if (response.status === 200) {
                        const updateddiscounts = discounts.filter(discount => !selectedDiscounts.includes(discount.dId));

                        // Cập nhật state để tái render bảng
                        setDiscounts(updateddiscounts);

                        // Xóa danh sách khách hàng đã chọn
                        setSelectedDiscounts([]);
                        toast.success('discounts deleted successfully');

                    } else {
                        toast.error('Failed to delete discounts');
                    }
                } catch (error) {
                    toast.error('Error deleting discounts: ' + error.message);
                }
            }
        }
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter') {
            handleSearch();
        }
    };


    const handleSearch = async () => {
        if (searchKeyword != "") {
            try {
                const response = await fetch(`/api/discount/SearchDiscounts?searchKeyword=${searchKeyword}`);
                const data = await response.json();
                setDiscounts(data);
            } catch (error) {
                console.error("Error searching customers:", error);
            }
        }
        else {
            try {
                const response = await fetch("/api/discount/GetDiscounts");
                const data = await response.json();
                setDiscounts(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        }
    };
    return (
        <div className="col-md-12 main">
  <div className="mt-md-6">
    <div className="navbar d-flex justify-content-between align-items-center">
      <h2 className="main-name mb-0">Thông tin mã giảm giá</h2>
      {/* Actions: Đổi mật khẩu và Xem thêm thông tin */}
      <div className="dropdown">
        <a className="d-flex align-items-center dropdown-toggle" href="#" role="button" id="userDropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          <i className="bi bi-person-circle" /> 
        </a>
        {/* Dropdown menu */}
        <div className="dropdown-menu" aria-labelledby="userDropdown">
          <a className="dropdown-item" href="password_KhachHang.html">Đổi mật khẩu</a>
          <a className="dropdown-item" href="profile_KhachHang.html">Xem thêm thông tin</a>
        </div>
      </div>
    </div>
    {/*thanh tìm kiếm với bộ lọc*/}
    <div className="find mt-5">
      <div className="d-flex w-100 justify-content-start align-items-center">
        <i className="bi bi-search" />
        <span className="first">
                            <input
                                className="form-control"
                                placeholder="Tìm kiếm ..."
                                value={searchKeyword}
                                onChange={(e) => setSearchKeyword(e.target.value)}
                                onKeyPress={handleKeyPress}
                            />
        </span>
        <span className="second">Filters <i className="bi bi-chevron-compact-down" /></span>
      </div>
    </div>
    <table className="table table-bordered">
      <thead>
        <tr>
          <th />
          <th>Mã khuyến mãi</th>
          <th>Tên khuyến mãi</th>
          <th>Ngày bắt đầu</th>
          <th>Ngày kết thúc</th>
          <th>Phần trăm giảm giá</th>
        </tr>
      </thead>
                    <tbody>
                        {currentItems.map((item) => (
                            <tr key={item.dId}>
                                <td contentEditable="true" className="choose">
                                    <input
                                        className="form-check-input"
                                        type="checkbox"
                                        onChange={() => handleCheckboxChange(item.dId)}
                                        checked={selectedDiscounts.includes(item.dId)}
                                    />
                                </td>
                                <td>{item.dId}</td>
                                <td>{item.dName}</td>
                                <td>{item.dStart}</td>
                                <td>{item.dFinish}</td>
                                <td>{item.dPercent}</td>
                            </tr>
                        ))}
                    </tbody>
    </table>
    {/*3 nut bam*/}
                <div className="d-flex justify-content-end my-3">
                    <button className="btn btn-primary mr-2" id="btnThem" onClick={handleClick}>Thêm</button>
                    <button className="btn btn-danger mr-2" id="btnXoa" onClick={handleDelete}>Xóa</button>
                    <button className="btn btn-warning" id="btnSua" onClick={handleShowInfo}>Sửa</button>
    </div>
                <ul className="pagination justify-content-center">
                    <li className="page-item ">
                        <a className="page-link" tabIndex={-1} onClick={() => paginate(currentPage - 1)}>Previous</a>
                    </li>
                    {[...Array(Math.ceil(discounts.length / itemsPerPage)).keys()].map((number) => (
                        <li key={number} className={`page-item ${number + 1 === currentPage ? 'active' : ''}`}>
                            <a className="page-link" onClick={() => paginate(number + 1)}>{number + 1}</a>
                        </li>
                    ))}
                    <li className="page-item">
                        <a className="page-link" onClick={() => paginate(currentPage + 1)}>Next</a>
                    </li>
                </ul>
  </div>
</div>


    );
}
export default MaGiamGia;