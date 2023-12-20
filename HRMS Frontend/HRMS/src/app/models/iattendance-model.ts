export interface IAttendanceModel {
    id: number|null,
    emp_ID: number,
    emp_Name: string,
    dept_Name: string,
    arrivalTime: Date,
    departureTime: Date,
    attendanceDate: Date
}
