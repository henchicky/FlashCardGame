<script setup lang="ts">
import { watch } from 'vue';
import { GameStatus } from '../models/requests';
import { GameState } from '../models/enums';

const props = defineProps<{ gameStatus: GameStatus }>()

function startTimer() {
    props.gameStatus.timer = 10
    setInterval(() => {
        if (props.gameStatus.timer > 0) {
            console.log(props.gameStatus.timer)
            props.gameStatus.timer--
        }
        if (props.gameStatus.timer == 0){
            props.gameStatus.gameStatus = GameState.End
        }
    }, 1000)
}

// onMounted(() => {
//     console.log(props.gameStatus.timer)
//     if(props.gameStatus.gameStatus == GameState.Start){
//         startTimer()
//     }
// })

// watch(() => props.gameStatus.gameStatus, () => {
//     console.log(props.gameStatus.gameStatus)
//     if(props.gameStatus.gameStatus == GameState.Start){
//         startTimer()
//     }
// })
</script>

<template><b>{{ gameStatus.timer }}</b> secs</template>

<style scoped></style>
