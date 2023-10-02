import { ChallengeType } from "./ChallengeType"

export interface MedalDto {
    medalName : string, 
    imageString : string 
    challengeTypeId : number
    challengeType: ChallengeType
}
