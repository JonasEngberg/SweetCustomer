import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerDto } from '../models/customer-dto.model';
import { CountryDto } from '../models/country-dto.model';
import { CreateCustomerDto } from '../models/create-customer-dto.model';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
  private baseApiUrl = 'https://localhost:7166/api';

  constructor(private http: HttpClient) { }

  addCustomer(customer: CreateCustomerDto): Observable<any> {
    return this.http.post(`${this.baseApiUrl}/customer`, customer);
  }

  getCustomers(): Observable<CustomerDto[]> {
    return this.http.get<CustomerDto[]>(`${this.baseApiUrl}/customer`);
  }

  deleteCustomer(customerId: number) {
    return this.http.delete(`${this.baseApiUrl}/customer/${customerId}`);
  }

  getCountries(): Observable<CountryDto[]> {
    return this.http.get<CountryDto[]>(`${this.baseApiUrl}/country`);
  }
}

