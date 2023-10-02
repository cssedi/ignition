import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SocialFeedComponent } from './challenger/social-feed/social-feed.component';
import { HomeComponent } from './home/home.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { LibraryComponent } from './library/library.component';
import { DepartmentsComponent } from './admin/departments/departments.component';
import { AppComponent } from './app.component';
import { FunctionsComponent } from './admin/functions/functions.component';
import { EmojisComponent } from './admin/emojis/emojis.component';
import { InboxComponent } from './admin/inbox/inbox.component';
import { RewardArchitectsComponent } from './admin/reward-architects/reward-architects.component';
import { UsersComponent } from './admin/users/users.component';
import { ChallengeTypesComponent } from './admin/challenge-types/challenge-types.component';
import { RewardCategoryComponent } from './admin/reward-category/reward-category.component';
import { MedalsComponent } from './admin/medals/medals.component';
import { ReportsComponent } from './admin/reports/reports.component';
import { ChallengesComponent } from './admin/challenges/challenges.component';
import { CreateChallengeComponent } from './admin/create-challenge/create-challenge.component';
import { LoginComponent } from './Auth/login/login.component';
import { ForgotPasswordComponent } from './Auth/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './Auth/reset-password/reset-password.component';
import { RegisterComponent } from './Auth/register/register.component';
import { AuthService } from './services/auth.service';
import { UserChallengesComponent } from './challenger/user-challenges/user-challenges.component';
import { CompleteChallengeComponent } from './challenger/complete-challenge/complete-challenge.component';
import { RewardArchitectDashboardComponent } from './reward-architect-dashboard/reward-architect-dashboard.component';
import { RewardShopComponent } from './challenger/reward-shop/reward-shop.component';
import { ViewSubmisionsComponent } from './reward-architect/view-submisions/view-submisions.component';
import { CreatePrizeComponent } from './reward-architect/create-prize/create-prize.component';
import { AdminGaurdService } from './guards/admin-gaurd.service';
import { ChallengerGaurdService } from './guards/challenger-gaurd.service';
import { OrdersComponent } from './admin/orders/orders.component';
import { ViewRewardShopComponent } from './admin/view-reward-shop/view-reward-shop.component';
import { UpdateRewardComponent } from './admin/update-reward/update-reward.component';
import { FAQsComponent } from './admin/faqs/faqs.component';
import { ProfileComponent } from './challenger/profile/profile.component';
import { OtherProfileComponent } from './challenger/other-profile/other-profile.component';
import { TestComponent } from './test/test.component';
import { MyOrdersComponent } from './challenger/my-orders/my-orders.component';
import { ChallengeReportsComponent } from './reward-architect/challenge-reports/challenge-reports.component';
import { LeaderBoardComponent } from './challenger/leader-board/leader-board.component';
import { SupplierPlacedOrdersComponent } from './admin/supplier-placed-orders/supplier-placed-orders.component';
import { HelpDashComponent } from './admin/help-dash/help-dash.component';
import { AuditTrailComponent } from './admin/audit-trail/audit-trail.component';
import { ViewCalendarComponent } from './view-calendar/view-calendar.component';
import { HelpPageComponent } from './help-page/help-page.component';
import { DatabaseComponent } from './admin/database/database.component';
import { EditChallengeComponent } from './reward-architect/edit-challenge/edit-challenge.component';

const routes: Routes = [ 
  {path: '', component : HomeComponent},
  {path: 'login', component : LoginComponent},
  {path: 'home', component : HomeComponent},
  {path: 'social-feed', component : SocialFeedComponent,canActivate: [ChallengerGaurdService]},
  {path: 'departments', component : DepartmentsComponent},
  {path: 'functions', component : FunctionsComponent},
  {path: 'emojis', component : EmojisComponent},
  {path: 'inbox', component : InboxComponent},
  {path: 'reward-architects', component : RewardArchitectsComponent},
  {path: 'users', component : UsersComponent, canActivate: [AdminGaurdService]},
  {path: 'challenge-types', component : ChallengeTypesComponent},
  {path: 'reward-category', component : RewardCategoryComponent},
  {path: 'medals', component : MedalsComponent},
  {path: 'reports', component : ReportsComponent},
  {path: 'challenges', component : ChallengesComponent},
  {path: 'create-challenge', component : CreateChallengeComponent},
  {path: 'forgot-password', component : ForgotPasswordComponent},
  {path: 'library', component : LibraryComponent, canActivate: [AuthService]},
  {path: 'register', component : RegisterComponent},
  {path: 'reset-password', component : ResetPasswordComponent},
  {path: 'user-challenges', component : UserChallengesComponent,canActivate: [ChallengerGaurdService]},
  {path: 'admin', component : AdminDashboardComponent},
  {path: 'complete-challenge/:challengeId', component : CompleteChallengeComponent},
  {path: 'complete-challenge/:challengeId', component : CompleteChallengeComponent},
  {path: 'reward-shop', component : RewardShopComponent},
  {path: 'submitions', component :  ViewSubmisionsComponent},
  {path: 'create-prize', component :  CreatePrizeComponent},
  {path: 'orders', component :  OrdersComponent},
  {path: 'view-rewards', component :  ViewRewardShopComponent},
  {path: 'update-reward/:rewardId', component :  UpdateRewardComponent},
  {path: 'view-faqs', component :  FAQsComponent},
  {path: 'profile', component :  ProfileComponent},
  {path: 'other-profile/:challengerId', component :  OtherProfileComponent},
  {path: 'reward-architect-challenges', component :  ChallengesComponent},
  {path: 'test', component :  TestComponent},
  {path: 'my-orders', component :  MyOrdersComponent},
  {path: 'challenge-reports', component :  ChallengeReportsComponent},
  {path: 'leader-board', component :  LeaderBoardComponent},
  {path: 'supplier-orders', component :  SupplierPlacedOrdersComponent},
  {path: 'help-dash', component :  HelpDashComponent},
  {path: 'audit', component :  AuditTrailComponent},
  {path: 'view-calendar', component :  ViewCalendarComponent},
  {path: 'help-page', component :  HelpPageComponent},
  {path: 'help-page', component :  HelpPageComponent},
  {path:'database', component:DatabaseComponent},
  {path: 'edit-challenge/:challengeId', component : EditChallengeComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

