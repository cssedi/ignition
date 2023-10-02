import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierPlacedOrdersComponent } from './supplier-placed-orders.component';

describe('SupplierPlacedOrdersComponent', () => {
  let component: SupplierPlacedOrdersComponent;
  let fixture: ComponentFixture<SupplierPlacedOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierPlacedOrdersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierPlacedOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
