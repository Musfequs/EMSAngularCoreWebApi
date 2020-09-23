"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EmployeeService = /** @class */ (function () {
    function EmployeeService(httpobj) {
        this.httpobj = httpobj;
        this.appPath = "";
    }
    EmployeeService.prototype.GetBaseUrl = function () {
        this.appPath = "http://localhost:63223/MyApp/counter";
        return this.appPath;
    };
    return EmployeeService;
}());
exports.EmployeeService = EmployeeService;
//# sourceMappingURL=employee.service.js.map