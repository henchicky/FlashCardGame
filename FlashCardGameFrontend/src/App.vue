<script setup lang="ts">
import { onBeforeMount, reactive, ref } from 'vue'
import { ElNotification, type FormInstance, type FormRules } from 'element-plus'
import axios from "axios"
import { GameDetails } from "./models/requests"
import { GameState } from "./models/enums"
import { operatorOptions } from "./services/dropdownOptions"
import { toOperatorSymbols } from "./services/enumExtensions"

interface RuleForm {
  input: number | null
}

const user = ref<string>(generateRandomUser())
const gameDetail = ref<GameDetails>({
  number1: null,
  number2: null,
  operator: null,
  score: 0
})
const operatorDropdown = ref<number[]>([0])
const timer = ref<number>(60)
const gameStatus = ref<GameState>(GameState.Pending);
let interval: number | undefined

const gameDetailRef = ref<FormInstance>()
const answer = reactive<RuleForm>({
  input: null
})
const rules = reactive<FormRules<RuleForm>>({
  input: [
    {
      required: true,
      message: 'Required!',
      trigger: 'blur'
    },
    {
      message: 'Non Number!',
      trigger: 'blur',
      type: "number"
    }
  ]
})

const submitForm = async (formEl: FormInstance | undefined) => {
  if (!formEl) return

  await formEl.validate((valid) => {
    if (valid) {
      axios.post<boolean>("http://localhost:5176/Game/Input", { input: answer.input, user: user.value })
        .then(res => {
          getGameDetails();
          if (res.data == false) {
            ElNotification({
              title: 'Error',
              message: 'Incorrect answer!',
              type: 'error',
            })
          }
        })
    }
  })
}

function getGameDetails() {
  axios.post<GameDetails>("http://localhost:5176/Game/GameDetails", { operatorTypes: operatorDropdown.value, user: user.value })
    .then(res => {
      gameDetail.value = res.data
      answer.input = null
    })
    .catch(err => console.error(err))
}

function startGame() {
  gameStatus.value = GameState.Start;
  timer.value = 60
  interval = setInterval(() => {
    if (timer.value > 0) {
      timer.value--
    }
    if (timer.value == 0) {
      gameStatus.value = GameState.End
      clearInterval(interval)
    }
  }, 1000)
}

const resetGame = (formEl: FormInstance | undefined) => {
  if (formEl)
    formEl.resetFields()
  gameStatus.value = GameState.Pending
  user.value = generateRandomUser()
  gameDetail.value.score = 0
  clearInterval(interval)
}

function generateRandomUser(){
  return Math.random().toString()
}

onBeforeMount(() => getGameDetails())
</script>

<template>
  <div v-if="gameStatus == GameState.Start">
    <el-form ref="gameDetailRef" :model="answer" :rules="rules" @keydown.enter="submitForm(gameDetailRef)"
      @submit.native.prevent>
      <b>{{ timer }}</b> secs
      <br />
      <br />
      <b>Score {{ gameDetail.score }}</b>

      <br />
      <br />
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
          <el-form-item prop="input">
            <el-input v-model.number="answer.input" autofocus/>
          </el-form-item>
        </el-col>
      </el-row>
      <br />
      <el-button @click="resetGame(gameDetailRef)" type="info">Start Over</el-button>
    </el-form>
  </div>
  <div v-else-if="gameStatus == GameState.Pending">
    <h2>Welcome to the Abacus 101</h2>
    <b>Choose the mathematical operations</b>
    <br />
    <br />
    <el-select v-model="operatorDropdown" multiple style="width: 300px;">
      <el-option v-for="item in operatorOptions" :key="item.value" :label="item.label" :value="item.value" />
    </el-select>
    <br />
    <br />
    <el-button type="primary" @click="startGame">Start Game</el-button>
    <br />
    <br />
    <el-card class="box-card">
      <template #header>
        <div>
          <span>Game Rules</span>
        </div>
      </template>
      <div>
        <span>- Correct attempt will be awarded 1 mark.</span>
        <br />
        <span>- Incorrect attempt will be deducted 1 mark.</span>
        <br />
        <span>- For division: round down to nearest whole number.</span>
        <br />
        <span>- <b>Enter</b> to submit you answer.</span>
      </div>
    </el-card>
  </div>
  <div v-else>
    <el-result icon="success" title="Congrats!" :sub-title="'You scored ' + gameDetail.score + ' points.'">
      <template #extra>
        <el-button type="primary" @click="resetGame(gameDetailRef)">Play again</el-button>
      </template>
    </el-result>
  </div>
</template>
