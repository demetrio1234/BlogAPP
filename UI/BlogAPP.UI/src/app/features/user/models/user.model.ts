export interface User {
    id?:string;
    name?: string;
    email: string;
    hashedPass?: string;
    address?: string;
    city?: string;
    region?: string;
    postalCode?: string;
    country?: string;
    phone?: string;
    roles:string[];
}