<template>
  <form @submit.prevent="onSubmit" class="parse">
    <textarea v-model="textForParsing" rows="10" cols="45" name="text"></textarea>
    <button type="submit" value="Parse!">Parse!</button>
  </form>
</template>

<script>
export default {
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
  }
};
</script>>