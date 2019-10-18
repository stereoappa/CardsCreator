<template>
  <div>
    <form @submit.prevent="onSubmit" class="parse">
      <div class="parse-input">
      <textarea v-model="textForParsing" rows="10" cols="45" name="text" placeholder="Put your pair of words here.."></textarea>
      </div>
      <button class="btn" type="submit" value="Parse!" :disabled="UIBlocked">Parse!</button>
      <Loader v-show="UIBlocked"/>
    </form>
  </div>
</template>

<script>
import Loader from "@/components/Loader";

export default {
  props: {UIBlocked : Boolean},
  data() {
    return {
      textForParsing: ""
    };
  },
  methods: {
    onSubmit() {
      if (this.textForParsing.trim()) {
        this.$emit("parse-cards", this.textForParsing);
        this.textForParsing = "";
      }
    }
  },
  components: {
    Loader
  }
};
</script>>

<style>
  .parse-input{
    /* border: 1px solid black; */
    border-radius: 18px;
    box-shadow: 2px 1px 4px rgba(0, 0, 0, 0.5);

    padding: 20px 13px 6px 15px;
    margin-bottom: 15px;

    
  background-image: url("../assets/img/kraftpaper.jpg");
  background-repeat: repeat;
  background-size: 100%;
  }

  .parse-input textarea{
    border: none;
    resize: vertical;
    outline: none;
    font-size: 14pt;
    min-height: 100px;
    max-height: 500px;
    /* padding: 25px 15px 15px 15px; */
    
  }
</style>