import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { CustomerService } from './services/customer.service';
import { CustomerDto } from './models/customer-dto.model';
import { CountryDto } from './models/country-dto.model';
import { CreateCustomerDto } from './models/create-customer-dto.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  createCustomer: CreateCustomerDto = {
    name: '',
    countryId: -1,
  };
  customers: Array<CustomerDto> = [];
  countries: Array<CountryDto> = [];

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.refresh();
  }

  save() {
    if (this.createCustomer.name.trim() !== '' && this.createCustomer.countryId > 0) {
      this.customerService.addCustomer(this.createCustomer).subscribe(
        () => {
          this.refresh();
        },
        (error: any) => {
          console.error('Error adding custmer:', error);
        }
      );
    }
  }

  delete(customerId: number) {
    console.log('Delete customer: ', customerId);
    this.customerService.deleteCustomer(customerId).subscribe(
      () => {
        this.refresh();
      },
      (error: any) => {
        console.error('Error deleting custmer:', error);
      }
    );
  }

  refresh() {
    console.log("refreshing");
    forkJoin([
      this.customerService.getCustomers(),
      this.customerService.getCountries()
    ]).subscribe(
      ([customers, countries]: [CustomerDto[], CountryDto[]]) => {
        this.customers = customers;
        this.countries = countries;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }
}
