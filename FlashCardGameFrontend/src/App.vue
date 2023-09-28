<script setup lang="ts">
import { onBeforeMount, reactive, ref } from 'vue'
import type { FormInstance, FormRules } from 'element-plus'
import axios from "axios"
import { GameDetails } from "./models/requests"
import { OperatorType } from "./models/enums";

interface RuleForm {
  input: number | null
}

const gameDetail = ref<GameDetails>({
  number1: null,
  number2: null,
  operator: null,
  score: 0
})

const gameDetailRef = ref<FormInstance>()
const answer = reactive<RuleForm>({
  input: null
})

const rules = reactive<FormRules<RuleForm>>({
  input: [
    {
      required: true,
      message: 'Required!',
      trigger: 'blur',
      type: 'number'
    }
  ]
})

const submitForm = async (formEl: FormInstance | undefined) => {
  if (!formEl) return
  await formEl.validate((valid, fields) => {
    if (valid) {
      console.log('submit!')
      axios.post("http://localhost:5176/Game/Input")
    } else {
      console.log('error submit!', fields)
    }
  })
}

const resetGame = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
  axios.post("http://localhost:5176/Game/Reset")
}

function toOperatorSymbols(operatorType: OperatorType | null){
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

function getGameDetails(){
  axios.post<GameDetails>("http://localhost:5176/Game/GameDetails", { operatorTypes : [1], user : 'test'})
  .then(res => gameDetail.value = res.data)
  .catch(err => console.error(err))
}

onBeforeMount(() => getGameDetails())
</script>

<template>
  <el-form ref="gameDetailRef" :model="answer" :rules="rules">
    Score {{ gameDetail.score }}
    <el-row>
      <el-col :span="4">
        {{ gameDetail.number1 }}
      </el-col>
      <el-col :span="4">
        {{ toOperatorSymbols(gameDetail.operator) }}
      </el-col>
      <el-col :span="4">
        {{ gameDetail.number2 }}
      </el-col>
      <el-col :span="4">
        =
      </el-col>
      <el-col :span="8">
        <el-form-item prop="answer">
          <el-input-number v-model="answer.input" :controls="false" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-form-item>
      <el-button type="primary" @click="submitForm(gameDetailRef)">
        Submit
      </el-button>
      <el-button @click="resetGame(gameDetailRef)">Start Over</el-button>
    </el-form-item>
  </el-form>
</template>

<style scoped></style>
