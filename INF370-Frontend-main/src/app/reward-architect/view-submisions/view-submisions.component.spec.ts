import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSubmisionsComponent } from './view-submisions.component';

describe('ViewSubmisionsComponent', () => {
  let component: ViewSubmisionsComponent;
  let fixture: ComponentFixture<ViewSubmisionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewSubmisionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewSubmisionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
