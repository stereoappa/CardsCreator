<template>
  <div>
    <section>
      <label>
        <input v-model="mode" value="manuallyMode" name="addMode" type="radio" />Manually added
      </label>
      <label>
        <input v-model="mode" value="parseMode" name="addMode" type="radio" /> Parse your text
      </label>
    </section>
    <div>
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