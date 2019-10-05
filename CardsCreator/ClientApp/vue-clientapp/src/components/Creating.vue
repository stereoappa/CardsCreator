<template>
  <div class="creating-container">
      <div class="radio-group">
        
        <input v-model="mode" value="manuallyMode" id="manuallyModeOption" name="selector" type="radio" class="input-card">
        <label for="manuallyModeOption">Manual</label>

        <input v-model="mode" value="parseMode" id="parseModeOption" name="selector" type="radio" />
        <label for="parseModeOption">Parse</label>  
      </div>

      <div class="added-section">
        <ManuallyAdd @add-card="addCard" v-show="this.mode=='manuallyMode'" />
        <ParseAdd @parse-cards="parseCards" v-show="this.mode=='parseMode'" />
      </div>
  </div>
</template>

<script>
import ManuallyAdd from "@/components/ManuallyAdd";
import ParseAdd from "@/components/ParseAdd";

export default {
  data() {
    return {
      mode: "manuallyMode"
    };
  },
  methods: {
    addCard(card) {
      this.$emit("add-card", card);
    },
    parseCards(text) {
      this.$emit("parse-cards", text);
    }
  },
  components: {
    ManuallyAdd,
    ParseAdd
  }
};
</script>

<style >
.creating-container{
  margin-bottom: 30px;
}

.radio-group input[type=radio] {
  position: absolute;
  visibility: hidden;
  display: none;
}

.radio-group label {
  font-size: 11pt;
  width: 85px;
  color: #9b0000;
  display: inline-block;
  cursor: pointer;
  padding: 4px 18px;
}
.radio-group input[type=radio]:checked + label {
  color:#f7f7f7;
  background: #9b0000;
}
.radio-group label + input[type=radio] + label {
  border-left: solid 2px #9b0000;
}

.radio-group {
  border: solid 1px #9b0000;
  margin-right: 6px;
  display: inline-block;
  border-radius: 18px;
  overflow: hidden;

  box-shadow: 2px 1px 4px rgba(0,0,0,0.5);
}

.creating-inputs {
  border: 1px solid #ccc;
  /* border-radius: 18px; */
}

.added-section{
  width: 690px;
  margin: auto;
}

textarea::placeholder { /* Chrome, Firefox, Opera, Safari 10.1+ */
  color:#777272;
  opacity: 1; /* Firefox */
  /* font-size: 15pt; */
}

</style>