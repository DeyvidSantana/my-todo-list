<template>
  <div>
    <MyTodoList/>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import MyTodoList from '@/components/MyTodoList.vue'; // @ is an alias to /src

export default Vue.extend({
  name: 'Home',
  components: {
    MyTodoList,
  },
});
</script>
