export interface IRoleCliamsModel {
    roleName: string,
    claims:IClaims[]
}

export interface IClaims {
    pageName:string,
    permission:string
}