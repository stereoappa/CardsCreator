<template>
  <div class="manual-section">
    <form @submit.prevent="onSubmit">
      <div class="manual-form">
        <div class="side right-border">
          <textarea type="text" v-model="sideOneText" placeholder="Side one" />
        </div>

        <div class="side">
          <textarea type="text" v-model="sideTwoText" placeholder="Side two" />
        </div>
      </div>
      <button class="btn" type="submit">Create!</button>
    </form>
  </div>
</template>

<script>
import Card from "../models/card.js";
import LanguageType from "../models/languageType.js";
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

<style>
.manual-section {
}

.manual-form {
  display: flex;
  justify-content: space-between;
  height: 130px;
  border-radius: 18px;
  box-shadow: 2px 1px 4px rgba(0, 0, 0, 0.5);
}

.side {
  display: flex;
  align-items: center;
  justify-content: center;

  width: 50%;
  /* border-right: 3px dashed rgb(122, 116, 116); */
  /* border: 1px solid red; */
}
.right-border {
  border-right: 2px dashed #ccc;
}

.side textarea {
  /* border: 1px solid red; */
  border: none;
  resize: none;
  outline: none;

  /* padding: 15px; */
  padding: 25px 15px 15px 15px;
  margin: 0px 20px;

  text-align: center;
  word-break: break-all;

  font-size: 18pt;
  width: 100%;
  height: 100%;
  box-sizing: border-box; /* For IE and modern versions of Chrome */
  -moz-box-sizing: border-box; /* For Firefox                          */
  -webkit-box-sizing: border-box;
}

button {
  /* position: absolute; */
}
</style>
