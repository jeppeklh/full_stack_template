export interface PersonnelGroup {
  id: string;
  name: string;
  abbreviation: string;
  departmentId?: string | null;          // present on entity responses
}

export interface PersonnelGroupPayload {
  name: string;
  abbreviation: string;
  departmentId?: string | null;
}

export enum UserStatus {
  Active = 0,
  Inactive = 1,
}

export interface Personnel {
  id: string;
  initials: string;
  fullName: string;
  email: string;
  userStatus: UserStatus;
  departmentId?: string | null;
  departmentName?: string | null;
  doctorTypeId?: string | null;
  doctorTypeName?: string | null;
  permissionId?: string | null;
  permissionName?: string | null;
}

export interface PersonnelPayload {
  initials: string;
  fullName: string;
  email: string;
  userStatus?: UserStatus;
  departmentId?: string | null;
  doctorTypeId?: string | null;
  permissionId?: string | null;
}
