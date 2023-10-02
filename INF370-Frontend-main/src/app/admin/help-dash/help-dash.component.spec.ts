import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HelpDashComponent } from './help-dash.component';

describe('HelpDashComponent', () => {
  let component: HelpDashComponent;
  let fixture: ComponentFixture<HelpDashComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HelpDashComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HelpDashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
