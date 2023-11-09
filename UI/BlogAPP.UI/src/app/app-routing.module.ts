import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { BlogitemListComponent } from './features/blogitem/blogitem-list/blogitem-list.component';
import { AddBlogitemComponent } from './features/blogitem/add-blogitem/add-blogitem.component';
import { UserListComponent } from './features/user/user-list/user-list.component';
import { AddUserComponent } from './features/user/add-user/add-user.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { EditBlogitemComponent } from './features/blogitem/edit-blogitem/edit-blogitem.component';
import { ImageListComponent } from './shared/image-list/image-list.component';

const routes: Routes = [
  { path: 'admin/categories', component: CategoryListComponent },
  { path: 'admin/categories/add', component: AddCategoryComponent },
  { path: 'admin/categories/:id', component: EditCategoryComponent },

  { path: 'admin/blogItems', component: BlogitemListComponent },
  { path: 'admin/blogItems/add', component: AddBlogitemComponent },
  { path: 'admin/blogItems/:id', component: EditBlogitemComponent },
  
  { path: 'admin/images' , component: ImageListComponent },
  //{ path: 'admin/images/:id', component: ImageComponent },
  
  { path: 'admin/users', component: UserListComponent },
  { path: 'admin/users/add', component: AddUserComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
