import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PollAdminComponent } from './poll-admin.component';

describe('PollAdminComponent', () => {
  let component: PollAdminComponent;
  let fixture: ComponentFixture<PollAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PollAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PollAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
