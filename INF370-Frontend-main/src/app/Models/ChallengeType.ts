import { Challenge } from "./Challenge";

export interface ChallengeType {
    challengeTypeID: number;
    name: string;
    challenges: Challenge[];
}