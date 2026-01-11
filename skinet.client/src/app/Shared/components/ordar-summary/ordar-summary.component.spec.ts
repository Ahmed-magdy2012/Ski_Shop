import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdarSummaryComponent } from './ordar-summary.component';

describe('OrdarSummaryComponent', () => {
  let component: OrdarSummaryComponent;
  let fixture: ComponentFixture<OrdarSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrdarSummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdarSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
