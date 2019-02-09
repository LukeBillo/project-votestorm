import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PollAdminComponent } from './poll-admin.component';
import {MatCardModule, MatListModule} from '@angular/material';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('PollAdminComponent', () => {
  let component: PollAdminComponent;
  let fixture: ComponentFixture<PollAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PollAdminComponent ],
      imports: [
        MatCardModule,
        MatListModule,
        RouterTestingModule,
        HttpClientTestingModule,
      ]
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
