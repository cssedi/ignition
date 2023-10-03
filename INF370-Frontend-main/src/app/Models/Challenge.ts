import { ChallengeType } from "./ChallengeType";
import { MedalDto } from "./medal-dto";
import { Prize } from "./prize";
import { RegisterModel } from "./register-model";


export interface Challenge {
    challengeID: number;
    name: string;
    description: string;
    tokens: number;
    endDate: Date;
    startDate: Date;
    challengeTypeId: number;
    prizeId: number |null;
    medalId: number;
    image: string;
    countdown:string|null;
    challengeType:ChallengeType;
    medal: MedalDto;
    user: RegisterModel;
    prize: Prize;
    
  }
