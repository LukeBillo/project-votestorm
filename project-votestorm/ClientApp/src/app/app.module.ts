import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreatePollComponent } from './create-poll/create-poll.component';
import { GotoPollComponent } from './goto-poll/goto-poll.component';
import { PollComponent } from './poll/poll.component';
import {
  MatInputModule,
  MatButtonModule,
  MatSelectModule,
  MatRadioModule,
  MatCardModule,
  MatIconModule,
  MatListModule,
  MatMenuModule
} from '@angular/material';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LayoutModule } from '@angular/cdk/layout';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CreatePollComponent,
    NavBarComponent,
    GotoPollComponent,
    PollComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: ':pollId', component: PollComponent, pathMatch: 'full' }
    ]),
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    ReactiveFormsModule,
    LayoutModule,
    MatMenuModule,
    MatIconModule,
    MatListModule
  ],
  // TODO: factory provider for IConfig
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
