import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject } from "@angular/core";
import { Observable } from "rxjs";

export class EmployeeService
{
  public appUrl: string = "";

  httpOptions = {
    headers: new HttpHeaders({
      'content-type': 'application/json'
    })
  }


  constructor(private httpobj: HttpClient, @Inject("BASE_URL") _baseUrl: string)
  {
    this.appUrl = _baseUrl;
  }

  GetBaseUrl() {
    return this.appUrl;
  }

  GetAllEmployees(): Observable<EmployeeDetails>
  {
    return this.httpobj.get<EmployeeDetails>(this.appUrl +"Employee/api/v1/GetAllEmployees");
  }

  CreateEmployee(empDetails): Observable<EmployeeDetails>
  {
    console.log('EmployeeService.CreateEmployee ' + JSON.stringify(empDetails));
    return this.httpobj.post<EmployeeDetails>(this.appUrl + "Employee/api/v1/CreateEmployee",
      JSON.stringify(empDetails), this.httpOptions); 
  }


}

export class EmployeeDetails
{
  empId: string;
  empName: string;
  empAddress: string;
}




