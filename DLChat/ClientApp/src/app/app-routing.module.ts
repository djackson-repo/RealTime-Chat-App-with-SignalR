import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatRoomComponent } from './pages/chatRoom/chatRoom.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SearchComponent } from './pages/search/search.component';
import { newRoomComponent } from './pages/newRoom/newRoom.component';
const routes: Routes = [
  { path: 'newRoom', component: newRoomComponent },
  { path: 'home', component: HomeComponent},
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'chatRoom/:id', component: ChatRoomComponent },
  { path: 'search/:id', component: SearchComponent },];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
