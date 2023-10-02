import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SocialFeedComponent } from './challenger/social-feed/social-feed.component';
import { FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { LibraryComponent } from './library/library.component';
import { LoginComponent } from './Auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { DepartmentsComponent } from './admin/departments/departments.component';
import { FunctionsComponent } from './admin/functions/functions.component';
import { ForgotPasswordComponent } from './Auth/forgot-password/forgot-password.component';
import { EmojisComponent } from './admin/emojis/emojis.component';
import { InboxComponent } from './admin/inbox/inbox.component';
import { RewardArchitectsComponent } from './admin/reward-architects/reward-architects.component';
import { UsersComponent } from './admin/users/users.component';
import { ChallengeTypesComponent } from './admin/challenge-types/challenge-types.component';
import { ChallengeCategoriesComponent } from './admin/challenge-categories/challenge-categories.component';
import { RewardCategoryComponent } from './admin/reward-category/reward-category.component';
import { FAQsComponent } from './admin/faqs/faqs.component';
import { MedalsComponent } from './admin/medals/medals.component';
import { QuizCatergoryComponent } from './admin/quiz-catergory/quiz-catergory.component';
import { ReportsComponent } from './admin/reports/reports.component';
import { ChallengesComponent } from './admin/challenges/challenges.component';
import { CreateChallengeComponent } from './admin/create-challenge/create-challenge.component';
import { ResetPasswordComponent } from './Auth/reset-password/reset-password.component';
import { RegisterComponent } from './Auth/register/register.component';
import { UserChallengesComponent } from './challenger/user-challenges/user-challenges.component';
import { CompleteChallengeComponent } from './challenger/complete-challenge/complete-challenge.component';
import { RewardArchitectDashboardComponent } from './reward-architect-dashboard/reward-architect-dashboard.component';
import { RewardShopComponent } from './challenger/reward-shop/reward-shop.component';
import { ViewSubmisionsComponent } from './reward-architect/view-submisions/view-submisions.component';
import { CreatePrizeComponent } from './reward-architect/create-prize/create-prize.component';
import { OrdersComponent } from './admin/orders/orders.component';
import { ViewRewardShopComponent } from './admin/view-reward-shop/view-reward-shop.component';
import { UpdateRewardComponent } from './admin/update-reward/update-reward.component';
import { NgToastModule } from 'ng-angular-popup';
import { ProfileComponent } from './challenger/profile/profile.component';
import { OtherProfileComponent } from './challenger/other-profile/other-profile.component';
import { TestComponent } from './test/test.component';
import { MyOrdersComponent } from './challenger/my-orders/my-orders.component';
import { ChallengeReportsComponent } from './reward-architect/challenge-reports/challenge-reports.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { LeaderBoardComponent } from './challenger/leader-board/leader-board.component';
import { SupplierPlacedOrdersComponent } from './admin/supplier-placed-orders/supplier-placed-orders.component';
import { HelpDashComponent } from './admin/help-dash/help-dash.component';
import { AuditTrailComponent } from './admin/audit-trail/audit-trail.component';
import { ViewCalendarComponent } from './view-calendar/view-calendar.component';
import { FullCalendarComponent, FullCalendarModule } from '@fullcalendar/angular';
import { HelpPageComponent } from './help-page/help-page.component';
import { DatabaseComponent } from './admin/database/database.component';
import { EditChallengeComponent } from './reward-architect/edit-challenge/edit-challenge.component';
import { CommonModule, DatePipe } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    SocialFeedComponent,
    LibraryComponent,
    LoginComponent,
    HomeComponent,
    AdminDashboardComponent,
    DepartmentsComponent,
    FunctionsComponent,
    ForgotPasswordComponent,
    EmojisComponent,
    InboxComponent,
    RewardArchitectsComponent,
    UsersComponent,
    ChallengeTypesComponent,
    ChallengeCategoriesComponent,
    RewardCategoryComponent,
    FAQsComponent,
    MedalsComponent,
    QuizCatergoryComponent,
    ReportsComponent,
    ChallengesComponent,
    CreateChallengeComponent,
    ResetPasswordComponent,
    RegisterComponent,
    UserChallengesComponent,
    CompleteChallengeComponent,
    RewardArchitectDashboardComponent,
    RewardShopComponent,
    ViewSubmisionsComponent,
    CreatePrizeComponent,
    OrdersComponent,
    ViewRewardShopComponent,
    UpdateRewardComponent,
    ProfileComponent,
    OtherProfileComponent,
    TestComponent,
    MyOrdersComponent,
    ChallengeReportsComponent,
    LeaderBoardComponent,
    SupplierPlacedOrdersComponent,
    HelpDashComponent,
    AuditTrailComponent,
    ViewCalendarComponent,
    HelpPageComponent,
    HelpPageComponent,
    DatabaseComponent,
    EditChallengeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, 
    FormsModule,
    ReactiveFormsModule,
    NgToastModule,
    NgApexchartsModule,
    FullCalendarModule,
    CommonModule
  
    
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
