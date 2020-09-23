import { Component, OnInit } from '@angular/core';
import { EmployeeService, EmployeeDetails } from '../../services/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  public employeeDetails: EmployeeDetails;

  constructor(private employeeService: EmployeeService)
  {
    this.GetAllEmployees();
  }

  ngOnInit() {
  }

  GetAllEmployees()
  {
    this.employeeService.GetAllEmployees().subscribe(data => this.employeeDetails = data);
  }

}
