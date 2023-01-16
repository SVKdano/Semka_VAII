import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import {WelcomeComponent} from "./welcome/welcome.component";
import {LigaPatComponent} from "./liga-pat/liga-pat.component";
import {LigaSestComponent} from "./liga-sest/liga-sest.component";
import {LigaSedemComponent} from "./liga-sedem/liga-sedem.component";

const routes: Routes = [
  { path: "", component: WelcomeComponent },
  { path: "leagueFive", component: LigaPatComponent},
  { path: "leagueSix", component: LigaSestComponent},
  { path: "leagueSeven", component: LigaSedemComponent}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
