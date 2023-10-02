import { Challenge } from "./Challenge";

export interface ChallengeStatus {
    ChallengeStatusID: number;
    Name: string;
    Challenges: Challenge[];
}