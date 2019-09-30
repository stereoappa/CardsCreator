<template>
  <form @submit.prevent="onSubmit" class="add-card">
    <div>
      <label>
        Language one
        <select>s</select>
      </label>
      <label>
        Side one
        <input type="text" v-model="sideOneText" />
      </label>
    </div>
    <div>
      <label>
        Language two
        <select>s</select>
      </label>
      <label>
        Side two
        <input type="text" v-model="sideTwoText" />
      </label>
    </div>
    <button type="submit">Create!</button>
  </form>
</template>

<script>
import Card from '../models/card.js';
import LanguageType from '../models/languageType.js';
export default {
  data() {
    return {
      sideOneText: "",
      sideTwoText: ""
    };
  },
  methods: {
    onSubmit() {
      if (this.sideOneText.trim() && this.sideTwoText.trim()) {
        this.$emit("add-card", new Card(this.sideOneText, this.sideTwoText));
        this.sideOneText = "";
        this.sideTwoText = "";
      }
    }
  }
};
</script>>


<style scoped>
.add-card {
  border: 1px solid #ccc;
}
</style>