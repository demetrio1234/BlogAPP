import { Category } from "../../category/models/category.model";

export interface BlogItem {
    id: string;
    title: string;
    shortdescription: string;
    content: string;
    featuredimageurl: string;
    urlhandle: string;
    publisheddate: Date;
    author: string;
    isvisible: string;
    categories:Category[]
}