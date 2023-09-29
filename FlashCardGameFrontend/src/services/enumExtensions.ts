import { OperatorType } from "../models/enums"

export function toOperatorSymbols(operatorType: OperatorType | null) {
    switch (operatorType) {
      case OperatorType.Addition:
        return "+"
      case OperatorType.Subtraction:
        return "-"
      case OperatorType.Multiplication:
        return "x"
      case OperatorType.Division:
        return "÷"
      default:
        return "+"
    }
  }