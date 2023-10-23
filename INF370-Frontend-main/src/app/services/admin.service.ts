import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedalDto } from '../Models/medal-dto';
import { CreateUserDto } from '../Models/createUserDto';
import { Department } from '../Models/Department';
import { UnnassignedArchitectVM } from '../Models/UnassignedArchitectVM';
import { OrganisationalFunction } from '../Models/OrganisationalFunction';
// import { FunctionVM } from '../Models/FunctionVM';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor(private http: HttpClient) { }
  
  baseApiURL = "https://localhost:7269/"
  token = localStorage.getItem('token')
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`
    })
  };

  assignUserRole(userId: string, role: string): Observable<any> {
    var dto = {
      userId: userId,
      role: role
    }
    return this.http.post<any>('https://localhost:7269/api/Auth/AssignUserRole', dto, this.httpOptions)
  }

  getAllFunction(): Observable<OrganisationalFunction[]> {
    return this.http.get<OrganisationalFunction[]>('https://localhost:7269/api/Function/GetAllFunctions', this.httpOptions)
  }

  getFuntionById(id: number): Observable<OrganisationalFunction> {
    return this.http.get<OrganisationalFunction>('https://localhost:7269/api/Function/' + id)

  }

  getSuperArchitects(): Observable<any[]> {
    return this.http.get<any>('https://localhost:7269/api/Auth/GetAllUsers')
  }

  updateFunction(id: number, functionObj: OrganisationalFunction): Observable<OrganisationalFunction> {
    return this.http.put<OrganisationalFunction>('https://localhost:7269/api/Function/' + id, functionObj, this.httpOptions)

  }

  deleteFunction(id: number) {
    return this.http.delete<any>('https://localhost:7269/api/Function/DeleteFunction/' + id, this.httpOptions)

  }

  addFunction(functionObj: any): Observable<any> {
    return this.http.post<any>('https://localhost:7269/api/Function/AddFunction', functionObj, this.httpOptions)

  }

  //CRUD department
  addDepartment(departmentObj: Department): Observable<Department> {
    return this.http.post<Department>(this.baseApiURL + "api/Department/CreateDep", departmentObj, this.httpOptions)
  }

  getDepartmentById(id: number): Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/Department/GetDepByID/' + id)
  }

  // Not sure why this is here, I might have been messing around with the return types
  getAllDepartmentsMain(): Observable<Department[]> {
    return this.http.get<Department[]>('https://localhost:7269/api/Department/GetAllDepartment')
  }

  getAllDepartments(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Department/GetAllDepartment')
  }

  updateDepartment(id: number, objct: Department): Observable<any> {
    return this.http.put<any>('https://localhost:7269/api/Department/UpdateDep/' + id, objct, this.httpOptions)

  }

  deleteDepartment(id: number): Observable<Department> {
    return this.http.delete<Department>('https://localhost:7269/api/Department/DeleteDep/' + id, this.httpOptions)
  }

  getSuperArchitectDepartments(id: string): Observable<Department[]> {
    return this.http.get<Department[]>('https://localhost:7269/api/Department/GetSuperArchitectDepartments/' + id, this.httpOptions)
  }
  //end CRUD department

  getAllRewardCategories(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/PrizeCategory', this.httpOptions)

  }

  getAllChallengeTypes(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/ChallengeTypes', this.httpOptions)

  }

  getAllChallengers(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Auth/GetAllChallengers')

  }

  getChallengeTypeById(id: number) {
    return this.http.get<any>('https://localhost:7269/api/ChallengeTypes/' + id)
  }

  getRewardCategoryById(id: number) {
    return this.http.get<any>('https://localhost:7269/api/PrizeCategory/' + id)
  }

  updateChallengeType(challengeType: any): Observable<any> {
    return this.http.put<any>('https://localhost:7269/api/ChallengeTypes/' + challengeType.id, { name: challengeType.name }, this.httpOptions)
  }

  updateRewardCategory(categoryType: any): Observable<any> {
    return this.http.put<any>('https://localhost:7269/api/PrizeCategory/' + categoryType.id, { PrizeCategoryName: categoryType.name })

  }

  addChallengeType(challengeType: any): Observable<any> {
    return this.http.post<any>('https://localhost:7269/api/ChallengeTypes', { name: challengeType.name }, this.httpOptions)
  }

  addRewardCategory(rewardCategory: any): Observable<any> {
    return this.http.post<any>('https://localhost:7269/api/PrizeCategory', { prizeCategoryName: rewardCategory.name })
  }

  deleteRewardCategory(id: number): Observable<any> {
    return this.http.delete<any>('https://localhost:7269/api/PrizeCategory/' + id)
  }


  deleteChallengeType(id : number):Observable<any>{
    return this.http.delete<any>('https://localhost:7269/api/ChallengeTypes/' + id, this.httpOptions)
  }

  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Auth/GetAllUsers')
  }

  getAllMedal(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Medal', this.httpOptions)
  }

  createMedal(medal: any): Observable<any> {

    return this.http.post<any>('https://localhost:7269/api/Medal/', medal, this.httpOptions)
  }

  EditMedel(medalId: number, medal: MedalDto): Observable<MedalDto> {
    return this.http.put<MedalDto>('https://localhost:7269/api/Medal/' + medalId, medal, this.httpOptions)
  }

  getMedalById(medalId: number): Observable<MedalDto> {
    return this.http.get<MedalDto>('https://localhost:7269/api/Medal/GetMedalById/' + medalId, this.httpOptions)
  }

  getMedalsByChallengeType(id: number): Observable<MedalDto[]> {
    return this.http.get<MedalDto[]>('https://localhost:7269/api/Medal/GetMedalsByChallengeType/' + id, this.httpOptions)
  }

  deleteMedal(medalId: number): Observable<any> {
    return this.http.delete<any>('https://localhost:7269/api/Medal/' + medalId, this.httpOptions)
  }

  getAllPrizeOrders(): Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/PrizeOrder/GetOrdersAdmin')
  }

  leaderBoard(): Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/Auth/LeaderBoard')
  }

  removeUserRole(id: string, role: string): Observable<any> {
    var dto = {
      userId: id,
      role: role
    }
    return this.http.post<any>('https://localhost:7269/api/Auth/RemoveUserRole', dto, this.httpOptions)

  }

  createUser(user: CreateUserDto): Observable<any> {
    return this.http.post<any>('https://localhost:7269/api/Auth/CreateUser', user, this.httpOptions)
  }

  //CRUD Reward Types
  // ------------------------------------------------------------------//
  getAllPrizeTypes(): Observable<any> {
    return this.http.get<any[]>(this.baseApiURL + "api/Reward/GetAllRewardTypes")
  }

  getRewardTypeById(id: number) {
    return this.http.get<any>(this.baseApiURL + "api/Reward/GetRewardTypeById/" + id, this.httpOptions)
  }

  updateRewardType(categoryType: any): Observable<any> {
    return this.http.put<any>(this.baseApiURL + categoryType.id, { PrizeCategoryName: categoryType.name })
  }

  deleteRewardType(id: number): Observable<any> {
    return this.http.delete<any>(this.baseApiURL + "api/Reward/DeleteRewardType/" + id, this.httpOptions)
  }

  addRewardType(rewardCategory: any): Observable<any> {
    return this.http.post<any>(this.baseApiURL + "api/PrizeCategory", { prizeCategoryName: rewardCategory.name }, this.httpOptions)
  }

  GetUnAssignedAwardsArchitects(): Observable<UnnassignedArchitectVM[]> {
    return this.http.get<UnnassignedArchitectVM[]>(this.baseApiURL + "api/Auth/GetUnAssignedAwardsArchitects")

  }

  GetUnAssignedSuperArchitects(): Observable<UnnassignedArchitectVM[]> {
    return this.http.get<UnnassignedArchitectVM[]>(this.baseApiURL + "api/Auth/GetUnAssignedSuperArchitects")

  }

  createSupplierOrder(orders: any[]): Observable<any> {
    let prizeOrderVMs: any[] = []
    orders.forEach(order => {
      prizeOrderVMs.push({
        prizeId: order.prizeOrderId,
      })
    });
    return this.http.post<any>('https://localhost:7269/api/SupplierOrder/CreateSupplierOrder', prizeOrderVMs)
  }

  getSupplierOrder(): Observable<any> {
    return this.http.get<any>('https://localhost:7269/api/SupplierOrder/GellSupplierOrder', this.httpOptions)
  }

  getAllAudits(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Audit/GetAllAudits')


  }
}
