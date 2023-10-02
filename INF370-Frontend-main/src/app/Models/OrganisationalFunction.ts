import { Department } from "./Department";
import { UnnassignedArchitectVM } from "./UnassignedArchitectVM";

export interface OrganisationalFunction{
    functionId: number;
    functionCode: string;
    functionName: string;
    superArchitectId: string | null;
    departments: Department[]
    superArchitectName: string
    superArchitect: UnnassignedArchitectVM

}