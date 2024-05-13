import React, { useEffect, useState } from "react";
import './KhachHang.css';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';

const KhachHang = () => {
    const [searchKeyword, setSearchKeyword] = useState('');
    const [customers, setCustomers] = useState([]);
    const [selectedCustomers, setSelectedCustomers] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(8);
    const navigate = useNavigate();
    const [showConfirmation, setShowConfirmation] = useState(false);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = customers.slice(indexOfFirstItem, indexOfLastItem);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleClick = () => {
        navigate('/KhachHang_Them'); 
    };

    


    useEffect(() => {
        // Lấy danh sách khách hàng từ API hoặc nguồn dữ liệu khác
        const fetchData = async () => {
            try {
                const response = await fetch("/api/customer/GetCustomers");
                const data = await response.json();
                setCustomers(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);

    
    const handleCheckboxChange = (customerId) => {
        if (selectedCustomers.includes(customerId)) {
            // Nếu đã chọn thì hủy chọn
            setSelectedCustomers([]);
        } else {
            // Nếu chưa chọn thì chọn
            setSelectedCustomers([customerId]);
        }
    };

  

    const handleShowInfo = async () => {
        try {
            if (selectedCustomers.length > 0) {
                const response = await fetch(`/api/customer/GetCustomerDetails?cIds=${selectedCustomers.join(',')}`);
                const data = await response.json();

                // Chuyển hướng sang trang sửa khách hàng và truyền thông tin khách hàng
                navigate('/KhachHang_Sua', { state: { selectedCustomerInfo: data } });
            } else {
                console.log("No customers selected.");
            }
        } catch (error) {
            console.error("Error fetching customer details:", error);
        }
    };

    const divHandleDelete = () => {
        if (selectedCustomers.length > 0) {
            setShowConfirmation(true);
        } else {
            toast.warning('No customers selected for deletion');
        }
    };

    const handleDelete = async () => {
        setShowConfirmation(false);
        if (selectedCustomers.length > 0) {
                try {
                    const response = await axios.delete('http://localhost:44430/api/customer', {
                        data: selectedCustomers, // Pass the array as data
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    });

                    if (response.status === 200) {
                        const updatedCustomers = customers.filter(customer => !selectedCustomers.includes(customer.cId));

                        // Cập nhật state để tái render bảng
                        setCustomers(updatedCustomers);

                        // Xóa danh sách khách hàng đã chọn
                        setSelectedCustomers([]);
                        toast.success('Customers deleted successfully');

                    } else {
                        toast.error('Failed to delete customers');
                    }
                } catch (error) {
                    toast.error('Error deleting customers: ' + error.message);
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
                const response = await fetch(`/api/customer/SearchCustomers?searchKeyword=${searchKeyword}`);
                const data = await response.json();
                setCustomers(data);
            } catch (error) {
                console.error("Error searching customers:", error);
            }
        }
        else {
            try {
                const response = await fetch("/api/customer/GetCustomers");
                const data = await response.json();
                setCustomers(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        }
    };


    return (
        <div className="col-md-12 main">
            <div className=" mt-md-6">
                <div className="navbar d-flex justify-content-between align-items-center">
                    <h2 className="main-name mb-0">Khách hàng</h2>
                    {/* Actions: Đổi mật khẩu và Xem thêm thông tin */}
                    
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
       
      </div>
    </div>
    <table className="table table-bordered">
      <thead>
        <tr>
          <th />
          <th>Mã khách hàng</th>
          <th>Số CCCD</th>
          <th>Tên khách hàng</th>
          <th>Điểm tích lũy</th>
          <th>Email</th>
        </tr>
      </thead>
                    <tbody>
                        {currentItems.map((item) => (
                            <tr key={item.cId}>
                                <td contentEditable="true" className="choose">
                                    <input
                                        className="form-check-input"
                                        type="checkbox"
                                        onChange={() => handleCheckboxChange(item.cId)}
                                        checked={selectedCustomers.includes(item.cId)}
                                    />
                                </td>
                                <td>{item.cId}</td>
                                <td>{item.numId}</td>
                                <td>{item.fullname}</td>
                                <td>{item.point}</td>
                                <td>{item.mail}</td>
                            </tr>
                        ))}
      </tbody>
                </table>
                {showConfirmation && (
                    <div className="confirmation-dialog">
                        <p>Bạn chắc chắn muốn xóa khách hàng?</p>
                        <button className="yes" onClick={handleDelete}>Có</button>
                        <button className="no" onClick={() => setShowConfirmation(false)}>Không</button>
                    </div>
                )}
    {/*3 nut bam*/}
    <div className="d-flex justify-content-end my-3">
                    <button className="btn btn-primary mr-2" id="btnThem" onClick={handleClick}>Thêm</button>
                    <button className="btn btn-danger mr-2" id="btnXoa" onClick={divHandleDelete}>Xóa</button>
                    <button className="btn btn-warning" id="btnSua" onClick={handleShowInfo}>Sửa</button>
    </div>
                <ul className="pagination justify-content-center">
                    <li className="page-item ">
                        <a className="page-link" tabIndex={-1} onClick={() => paginate(currentPage - 1)}>Previous</a>
                    </li>
                    {[...Array(Math.ceil(customers.length / itemsPerPage)).keys()].map((number) => (
                        <li key={number} className={`page-item ${number + 1 === currentPage ? 'active' : ''}`}>
                            <a className="page-link"  onClick={() => paginate(number + 1)}>{number + 1}</a>
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
export default KhachHang;