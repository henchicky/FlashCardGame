import { OperatorType } from "./enums";

export interface GameDetails{
    number1: number | null,
    number2: number | null,
    operator: OperatorType | null,
    score: number,
}