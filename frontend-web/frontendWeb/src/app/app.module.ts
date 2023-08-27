import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OverviewMainComponent } from './overview-main/overview-main.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { HeaderComponent } from './components/header/header.component';
import { ButtonComponent } from './components/button/button.component';
import { PopupbarOverviewComponent } from './popupbar-overview/popupbar-overview.component';
import { CocktailOverviewComponent } from './cocktail-overview/cocktail-overview.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputComponent } from './components/input/input.component';
import { LabelComponent } from './components/label/label.component';
import { PopupbarsComponent } from './components/popupbars/popupbars.component';
import { PopupbarItemComponent } from './components/popupbar-item/popupbar-item.component';
import { AddPopupbarComponent } from './components/add-popupbar/add-popupbar.component';
import { PopupbarAddComponent } from './popupbar-add/popupbar-add.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddCocktailComponent } from './components/add-cocktail/add-cocktail.component';
import { CocktailAddComponent } from './components/cocktail-add/cocktail-add.component';
import { SearchFilterPipe } from './search-filter.pipe';
import { CocktailMenuComponent } from './cocktail-menu/cocktail-menu.component';
import { CocktailsComponent } from './components/cocktails/cocktails.component';
import { AddCocktailMenuComponent } from './components/add-cocktail-menu/add-cocktail-menu.component';
import { CocktailMenuAddComponent } from './components/cocktail-menu-add/cocktail-menu-add.component';
import { AuthGuard } from './auth/auth-guard.service';
import { CocktailEditFixComponent } from './cocktail-edit-fix/cocktail-edit-fix.component';

const appRoutes: Routes = [
  { path: '', component: OverviewMainComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'popupbar', component: PopupbarOverviewComponent, canActivate: [AuthGuard] },
  { path: 'cocktail', component: CocktailOverviewComponent, canActivate: [AuthGuard] },
  { path: 'cocktailmenu', component: CocktailMenuComponent, canActivate: [AuthGuard] },
  { path: 'cocktailedit', component: CocktailEditFixComponent, canActivate: [AuthGuard] },
]

@NgModule({
  declarations: [
    AppComponent,
    OverviewMainComponent,
    LoginPageComponent,
    HeaderComponent,
    ButtonComponent,
    PopupbarOverviewComponent,
    CocktailOverviewComponent,
    InputComponent,
    LabelComponent,
    PopupbarsComponent,
    PopupbarItemComponent,
    AddPopupbarComponent,
    PopupbarAddComponent,
    AddCocktailComponent,
    CocktailAddComponent,
    SearchFilterPipe,
    CocktailMenuComponent,
    CocktailsComponent,
    AddCocktailMenuComponent,
    CocktailMenuAddComponent,
    CocktailEditFixComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes), //, {enableTracing: true} For debugging
    ReactiveFormsModule,
    HttpClientModule,
    FontAwesomeModule,
    FormsModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
