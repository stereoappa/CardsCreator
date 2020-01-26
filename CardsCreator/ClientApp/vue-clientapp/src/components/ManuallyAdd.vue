<template>
  <div>
    <form @submit.prevent="onSubmit">
      <div class="manual-form">
        <div class="side right-border">
          <div class="input-field col s12 lang-selector">
            <select v-model="sideOneLang">
              <option value="1" selected>EN</option>
              <option value="2">RUS</option>
              <option value="0">AUTO</option>
            </select>
          </div>

          <div class="textblock">
            <textarea
              type="text"
              v-model="sideOneText"
              placeholder="Side one"
              @keyup.ctrl.enter="onSubmit"
              @keydown="getTranslate"
              tabindex="1"
            />
          </div>
        </div>
        <div class="side">
          <div class="input-field col s12 lang-selector">
            <select v-model="sideTwoLang">
              <option value="1">EN</option>
              <option value="2" selected>RUS</option>
            </select>
          </div>

          <div class="textblock">
            <textarea
              type="text"
              v-model="sideTwoText"
              placeholder="Side two"
              @keyup.ctrl.enter="onSubmit"
              tabindex="2"
            />
          </div>
        </div>
      </div>

      <div class="tooltip">
        <label>
          <button class="btn" type="submit">Create!</button>
          <span></span>
        </label>
        <span class="tooltip-toprint">or Ctrl + Enter</span>
      </div>
      <!-- <button class="btn" type="submit">Create!</button> -->
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
      sideTwoText: "",
      sideOneLang: 1,
      sideTwoLang: 2
    };
  },
  methods: {
    onSubmit() {
      if (this.sideOneText.trim() && this.sideTwoText.trim()) {
        this.$emit("add-card", new Card(this.sideOneText, this.sideTwoText, this.sideOneLang, this.sideTwoLang));
        this.sideOneText = "";
        this.sideTwoText = "";
      }
    },
    async getTranslate(){
      var sourceCard = new Card(this.sideOneText, '', this.sideOneLang, this.sideTwoLang)
      var apiUri = process.env.VUE_APP_API_URL

      this.$http({
        method: "post",
        url: apiUri + "translateCards",
        data: [sourceCard]
        })
        .then(response => {
          response.data.forEach(restoredCard => {
            if(restoredCard){
              this.sideTwoText = restoredCard.sideTwo.text
            }
          });
        })
        .catch(error => {
          console.log(error);
        })
    }
  }
};
</script>>

<style>
.manual-form {
  display: flex;
  justify-content: space-between;
  height: 130px;

  border-radius: 18px;
  box-shadow: 2px 1px 4px rgba(0, 0, 0, 0.5);
  margin-bottom: 15px;

  background-image: url("../assets/img/kraftpaper.jpg");
  background-repeat: no-repeat;
  background-size: 100%;
}

.side {
  display: flex;
  align-items: center;
  justify-content: center;

  width: 50%;
  padding: 0px 20px;
}
.right-border {
  border-right: 2px dashed #ccc;
}

.textblock {
  position: absolute;
}

.side textarea {
  /* border: 1px solid red; */
  border: none;
  resize: none;
  outline: none;

  /* padding: 15px; */
  padding: 25px 15px 15px 15px;

  text-align: center;
  word-break: break-all;

  font-size: 18pt;
  width: 100%;
  height: 100%;
  box-sizing: border-box; /* For IE and modern versions of Chrome */
  -moz-box-sizing: border-box; /* For Firefox                          */
  -webkit-box-sizing: border-box;
}

.lang-selector {
  width: 60px;
  position: absolute;
  right: -135px;
  top: -45px;
  z-index: 999;
}
</style>
