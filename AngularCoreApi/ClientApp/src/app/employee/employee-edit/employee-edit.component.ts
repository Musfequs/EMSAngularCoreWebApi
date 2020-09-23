import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from '../../../services/employee.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {

  submitted = false;
  private employeeaddform: FormGroup;

  constructor(public fb: FormBuilder, private router: Router, public employeeService: EmployeeService)
  {  }

  ngOnInit() {
    this.employeeaddform = this.fb.group(
      {
        empId: ['', Validators.required],
        empName: ['', Validators.required],
        empAddress: ['', Validators.required]
      }
    );
  }

  get f() { return this.employeeaddform.controls; }

  submitForm()
  {
    this.submitted = true;

    // stop here if form is invalid
    if (this.employeeaddform.invalid) {
      return;
    }

    console.log('Submit Started.' + this.employeeaddform.value);
    this.employeeService.CreateEmployee(this.employeeaddform.value).subscribe(res => {
      console.log('Employee is created successfully.');
    });
    this.employeeaddform.reset();
  }

  onReset() {
    this.submitted = false;
    this.employeeaddform.reset();
  }

}
